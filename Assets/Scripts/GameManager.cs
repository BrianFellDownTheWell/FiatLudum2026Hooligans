using System.Collections;
using UnityEngine;
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

    [Header("Scene Names")]
    [SerializeField] private string openingViewSceneName = "OpeningView";
    [SerializeField] private string gameOverSceneName = "GameOver";

    [Header("Dialogue")]
    [SerializeField] private Canvas dialogueCanvas;

    private bool levelFlowRunning;
    private bool waitingForMinigame;
    private bool minigameSuccess;
    private bool waitingForDialogue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Call from a scene script to kick off the current level's sequence.
    /// </summary>
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
            yield return SpawnAndWaitForMinigame(patchingHolesPrefab);
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

        // --- Story Dialogue ---
        yield return RunDialogue();

        // --- Advance to next level ---
        currentLevel++;
        levelFlowRunning = false;
        SceneManager.LoadScene(openingViewSceneName);
    }

    private IEnumerator SpawnAndWaitForMinigame(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("GameManager: minigame prefab not assigned, skipping.");
            minigameSuccess = true;
            yield break;
        }

        waitingForMinigame = true;
        minigameSuccess = false;

        GameObject instance = Instantiate(prefab);
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
