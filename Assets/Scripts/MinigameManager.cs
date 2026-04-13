using System;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public Action<bool> OnMinigameComplete;

    public void Win()
    {
        Debug.Log("Player won minigame");
        OnMinigameComplete?.Invoke(true);
        Destroy(gameObject);
    }
    public void Lose()
    {
        Debug.Log("Player lost minigame");
        OnMinigameComplete?.Invoke(false);
        Destroy(gameObject);
    }
}
