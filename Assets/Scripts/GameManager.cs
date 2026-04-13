using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int currentLevel = 1;
    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    [Header("Minigame Prefabs")]
    [SerializeField] private GameObject teaSetPrefab;
    [SerializeField] private GameObject patchingHolesPrefab;
    [SerializeField] private GameObject fanningFlamesPrefab;
    [SerializeField] private GameObject dodgingObstaclesPrefab;

    [Header("Story Files (one per level, index 0 = Level 1)")]
    [SerializeField] private TextAsset[] storyJSONPerLevel;

    [Header("Timing")]
    [SerializeField] private float delayBeforeFirstMinigame = 3f;
    [SerializeField] private float delayBetweenMinigames = 3f;
    [SerializeField] private float delayBeforeDialogue = 5f;

    [Header("Scene Names")]
    [SerializeField] private string openingViewSceneName = "OpeningView";
    [SerializeField] private string levelFlowSceneName = "View1";
    [SerializeField] private string gameOverSceneName = "FallEnding";
    [SerializeField] private string fallEndingSceneName = "FallEnding";
    [SerializeField] private string sunEndingSceneName = "SunEnding";
    [SerializeField] private string winSceneName = "WinScene";

    [Header("Dialogue")]
    [SerializeField] private string dialogueCanvasName = "DialogueCanvas";
    private Canvas dialogueCanvas;

    [Header("Events (per-level, fires after minigames complete)")]
    public UnityEvent OnLevel1Complete;
    public UnityEvent OnLevel2Complete;
    public UnityEvent OnLevel3Complete;
    public UnityEvent OnLevel4Complete;

    private bool levelFlowRunning;
    private bool waitingForMinigame;
    private bool minigameSuccess;
    private bool waitingForDialogue;
    private Canvas[] disabledCanvases;
    private string lastEnding;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == levelFlowSceneName)
        {
            // Re-find dialogue canvas in the new scene
            Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            dialogueCanvas = System.Array.Find(allCanvases, c => c.gameObject.name == dialogueCanvasName);

            StartLevelFlow();
        }
    }
    public void StartLevelFlow()
    {
        if (!levelFlowRunning)
            StartCoroutine(RunLevelFlow());
    }

    private IEnumerator RunLevelFlow()
    {
        levelFlowRunning = true;

        yield return new WaitForSeconds(delayBeforeFirstMinigame);

        // --- Tea Set (every level) ---
        yield return SpawnAndWaitForMinigame(teaSetPrefab);
        if (!minigameSuccess) { LoadGameOver(); yield break; }

        // --- Patching Holes (level 2+) ---
        if (currentLevel >= 2)
        {
            yield return new WaitForSeconds(delayBetweenMinigames);
            yield return SpawnAndWaitForMinigame(patchingHolesPrefab, false);
            if (!minigameSuccess) { LoadGameOver(); yield break; }
        }

        // --- Fanning Flames (level 3+) ---
        if (currentLevel >= 3)
        {
            yield return new WaitForSeconds(delayBetweenMinigames);
            yield return SpawnAndWaitForMinigame(fanningFlamesPrefab);
            if (!minigameSuccess) { LoadGameOver(); yield break; }
        }

        // --- Dodging Space Obstacles (level 4) ---
        if (currentLevel >= 4)
        {
            yield return new WaitForSeconds(delayBetweenMinigames);
            yield return SpawnAndWaitForMinigame(dodgingObstaclesPrefab);
            if (!minigameSuccess) { LoadGameOver(); yield break; }
        }

        // --- Grace period + transition animation ---
        switch (currentLevel)
        {
            case 1: OnLevel1Complete?.Invoke(); break;
            case 2: OnLevel2Complete?.Invoke(); break;
            case 3: OnLevel3Complete?.Invoke(); break;
            case 4: OnLevel4Complete?.Invoke(); break;
        }
        yield return new WaitForSeconds(delayBeforeDialogue);

        // --- Story Dialogue ---
        yield return RunDialogue();

        // --- Route based on ending ---
        levelFlowRunning = false;
        switch (lastEnding)
        {
            case "pop":
                SceneManager.LoadScene(fallEndingSceneName);
                break;
            case "good":
                SceneManager.LoadScene(winSceneName);
                break;
            case "bad":
                SceneManager.LoadScene(sunEndingSceneName);
                break;
            case "later":
            default:
                currentLevel++;
                SceneManager.LoadScene(openingViewSceneName);
                break;
        }
    }

    private IEnumerator SpawnAndWaitForMinigame(GameObject prefab, bool disableOtherCanvases = true)
    {
        if (prefab == null)
        {
            Debug.LogWarning("GameManager: minigame prefab not assigned, waiting 3 seconds.");
            yield return new WaitForSeconds(3f);
            minigameSuccess = true;
            yield break;
        }

        waitingForMinigame = true;
        minigameSuccess = false;

        GameObject instance = Instantiate(prefab);

        // Disable all existing scene canvases while the minigame is active
        if (disableOtherCanvases)
        {
            Canvas[] allCanvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
            Canvas minigameCanvas = instance.GetComponentInChildren<Canvas>();
            disabledCanvases = System.Array.FindAll(allCanvases, c => c != minigameCanvas && c.enabled);
            foreach (Canvas c in disabledCanvases)
                c.enabled = false;
        }

        MinigameManager mgr = instance.GetComponentInChildren<MinigameManager>();

        if (mgr != null)
        {
            mgr.OnMinigameComplete = HandleMinigameComplete;
        }
        else
        {
            Debug.LogError("GameManager: no MinigameManager found on prefab " + prefab.name);
            minigameSuccess = true;
            waitingForMinigame = false;
        }

        while (waitingForMinigame)
            yield return null;
    }

    private void HandleMinigameComplete(bool success)
    {
        // Re-enable scene canvases
        if (disabledCanvases != null)
        {
            foreach (Canvas c in disabledCanvases)
            {
                if (c != null) c.enabled = true;
            }
            disabledCanvases = null;
        }

        minigameSuccess = success;
        waitingForMinigame = false;
    }

    private IEnumerator RunDialogue()
    {
        if (storyJSONPerLevel == null || storyJSONPerLevel.Length == 0)
        {
            Debug.LogWarning("GameManager: no story JSONs assigned, skipping dialogue.");
            yield break;
        }

        int index = Mathf.Clamp(currentLevel - 1, 0, storyJSONPerLevel.Length - 1);
        TextAsset storyJSON = storyJSONPerLevel[index];

        if (storyJSON == null)
        {
            Debug.LogWarning("GameManager: story JSON for level " + currentLevel + " is null, skipping.");
            yield break;
        }

        InkDialoguePlayer dialoguePlayer = FindAnyObjectByType<InkDialoguePlayer>(FindObjectsInactive.Include);
        if (dialoguePlayer == null)
        {
            Debug.LogWarning("GameManager: no InkDialoguePlayer in scene, skipping dialogue.");
            yield break;
        }

        dialoguePlayer.dialogBox.SetActive(true);
        if (dialogueCanvas != null)
            dialogueCanvas.gameObject.SetActive(true);
        dialoguePlayer.EnterStoryFromJSONText(storyJSON.text);
        dialoguePlayer.ContinueStory();

        waitingForDialogue = true;
        dialoguePlayer.onStoryEnd.AddListener(OnDialogueEnd);

        while (waitingForDialogue)
            yield return null;

        dialoguePlayer.onStoryEnd.RemoveListener(OnDialogueEnd);

        // Read ending variable from Ink
        lastEnding = dialoguePlayer.currentStory.variablesState["ending"]?.ToString() ?? "later";

        if (dialogueCanvas != null)
            dialogueCanvas.gameObject.SetActive(false);
    }

    private void OnDialogueEnd()
    {
        waitingForDialogue = false;
    }

    private void LoadGameOver()
    {
        levelFlowRunning = false;
        SceneManager.LoadScene(gameOverSceneName);
    }
}   
