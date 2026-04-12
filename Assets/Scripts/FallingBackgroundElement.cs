using UnityEngine;

public class FallingBackgroundElement : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 4.0f;
    [SerializeField] private float lowerBound = -15.0f; // Where the object should stop being rendered
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform == null)
        {
            return;
        }
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }

}
