using UnityEngine;

public class ResetGameManager : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);
    }
}
