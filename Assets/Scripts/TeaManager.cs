using UnityEngine;
using TMPro;

public class TeaManager : MonoBehaviour
{
    [SerializeField] public int targetObjCount;

    private int currentObjCount = 0;

    [SerializeField] private float timerVal = 10.0f;
    [SerializeField] private TMP_Text timerText;

    private float currentTime;

    private MinigameManager minigameManager;

    void Start()
    {
        currentTime = timerVal;
        minigameManager = GetComponentInParent<MinigameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("timer value");

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (currentTime <= 0)
        {
            Debug.Log("Time's up!");
            if (minigameManager != null)
                minigameManager.Lose();
            enabled = false;
            return;
        }
        currentTime -= Time.deltaTime;
    }

    public void AddObj()
    {
        currentObjCount++;
        if (currentObjCount >= targetObjCount) {
            Debug.Log("Completed tea minigame");
            if (minigameManager != null)
                minigameManager.Win();
        }
    }
}
