using UnityEngine;

public class TeaManager : MonoBehaviour
{
    [SerializeField] public int targetObjCount;

    private int currentObjCount = 0;

    [SerializeField] private float timerVal = 10.0f;

    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = timerVal;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("timer value");
        Debug.Log(currentTime);

        if (currentTime <= 0)
        {
            Debug.Log("Time's up!");
        }
        currentTime -= Time.deltaTime;
    }

    public void AddObj()
    {
        currentObjCount++;
        if (currentObjCount >= targetObjCount) {
            Debug.Log("Completed tea minigame");
        }
    }
}
