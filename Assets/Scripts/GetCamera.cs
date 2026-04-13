using UnityEngine;

public class GetCamera : MonoBehaviour
{
    void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = Camera.main;
    }
}
