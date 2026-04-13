using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SquiggleManager : MonoBehaviour
{

    public List<GameObject> checkpoints;
    private int nextInd = 0;
    public bool isShapeDone = false;

    [SerializeField] private float timerVal = 30.0f;
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
            SceneManager.LoadScene("GameOver");
        }

        currentTime -= Time.deltaTime;
    }

    public void OnCheckpointHit(GameObject checkpunto)
    {
        if (isShapeDone)
        {
            Debug.Log("Already done with shape");
            return;
        }
        if(checkpunto == checkpoints[nextInd])
        {
            nextInd++;
            Debug.Log("Checkpoint done: " + nextInd);

            if(nextInd >= checkpoints.Count)
            {
                isShapeDone = true;
                OnSuccess();
            }
        }
        else
        {
            Debug.Log("Oops! You mislicked the checkpoints, so thou must start over.");
            nextInd = 0;
        }
    }

    void OnSuccess()
    {
        Debug.Log("Fully done with shape");
    }
}
