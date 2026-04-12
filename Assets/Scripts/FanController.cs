using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private int fanHealth = 150;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Fan health");
        Debug.Log(fanHealth);
        if (Input.GetKey(KeyCode.Space))
        {
            fanHealth -= 1;
        }
    }
}
