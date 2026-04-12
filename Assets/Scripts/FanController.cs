using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private int fanHealth = 150;
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

        if(currentTime <= 0)
        {
            Debug.Log("Time's up!");
            return;
        }

        Debug.Log("Fan health");
        Debug.Log(fanHealth);

        if (Input.GetKey(KeyCode.Space))
        {
            fanHealth -= 1;
        }

        currentTime -= Time.deltaTime;
    }
}
