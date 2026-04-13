using UnityEngine;
using UnityEngine.SceneManagement;

public class FanController : MonoBehaviour
{
    [SerializeField] public int fanHealth = 200;
    [SerializeField] public float timerVal = 30.0f;
    private float currentTime;

    public AudioManager am;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetFanDifficulty();
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
            SceneManager.LoadScene("GameOver");
        }

        Debug.Log("Fan health");
        Debug.Log(fanHealth);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fanHealth -= 1;
        }

        currentTime -= Time.deltaTime;
    }

    public void SetFanDifficulty()
    {
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
