using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FanController : MonoBehaviour
{
    [SerializeField] public int fanHealth = 100;
    [SerializeField] public float timerVal = 30.0f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text healthText;
    private float currentTime;

    [Header("Image Cycling")]
    [SerializeField] private Image imageA;
    [SerializeField] private Image imageB;
    [SerializeField] private Sprite[] spritesA = new Sprite[3];
    [SerializeField] private Sprite[] spritesB = new Sprite[3];
    private int cycleIndexA;
    private int cycleIndexB;

    public AudioManager am;
    private MinigameManager minigameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minigameManager = GetComponentInParent<MinigameManager>();
        if (minigameManager == null)
            minigameManager = GetComponentInChildren<MinigameManager>();
        SetFanDifficulty();
        currentTime = timerVal;
        UpdateTimerDisplay();
        UpdateHealthDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("timer value");
        Debug.Log(currentTime);

        if(currentTime <= 0)
        {
            Debug.Log("Time's up!");
            if (minigameManager != null)
                minigameManager.Lose();
            else
                SceneManager.LoadScene("FallEnding");
            return;
        }

        Debug.Log("Fan health");
        Debug.Log(fanHealth);
        if (fanHealth <= 0)
        {
            Debug.Log("Completed fan minigame");
            minigameManager?.Win();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fanHealth -= 1;
            CycleImages();
            UpdateHealthDisplay();
        }

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Max(currentTime, 0f);
        UpdateTimerDisplay();
    }

    private void CycleImages()
    {
        if (spritesA != null && spritesA.Length > 0)
        {
            cycleIndexA = (cycleIndexA + 1) % spritesA.Length;
            if (imageA != null) imageA.sprite = spritesA[cycleIndexA];
        }

        if (spritesB != null && spritesB.Length > 0)
        {
            cycleIndexB = (cycleIndexB + 1) % spritesB.Length;
            if (imageB != null) imageB.sprite = spritesB[cycleIndexB];
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
            healthText.text = "Left: " + fanHealth;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        }
    }

    public void SetFanDifficulty()
    {
        if (am == null)
        {
            Debug.LogWarning("FanController: AudioManager not assigned, using defaults.");
            return;
        }

        // Get the level from the audio manager
        int level = am.getLevel();

        // Set the difficulty for the fan depending on the level;
        // harder difficulties mean more fan health and a shorter
        // time limit
        if (level == 1)
        {
            fanHealth = 100;
            timerVal = 40.0f;
        }
        if (level == 2)
        {
            fanHealth = 150;
            timerVal = 35.0f;
        }
        if (level == 3)
        {
            fanHealth = 200;
            timerVal = 30.0f;
        }

        Debug.Log("Fan health and timer: " + fanHealth + ", " + timerVal);
    }
}
