using UnityEngine;
using System.Collections.Generic;

public class SquiggleManager : MonoBehaviour
{

    public List<GameObject> checkpoints;
    private int nextInd = 0;
    public bool isShapeDone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
