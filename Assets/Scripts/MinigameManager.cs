using System;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    /// <summary>
    /// Set by GameManager when the minigame is spawned.
    /// Passes true for success, false for failure.
    /// </summary>
    public Action<bool> OnMinigameComplete;

    /// <summary>
    /// Call this from minigame logic when the player wins.
    /// Destroys the minigame canvas.
    /// </summary>
    public void Win()
    {
        Debug.Log("Player won minigame");
        OnMinigameComplete?.Invoke(true);
        Destroy(gameObject);
    }

    /// <summary>
    /// Call this from minigame logic when the player loses.
    /// Destroys the minigame canvas.
    /// </summary>
    public void Lose()
    {
        Debug.Log("Player lost minigame");
        OnMinigameComplete?.Invoke(false);
        Destroy(gameObject);
    }
}
