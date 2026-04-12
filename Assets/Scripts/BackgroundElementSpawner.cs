using UnityEngine;

public class BackgroundElementSpawner : MonoBehaviour
{

    public GameObject backgroundElement;
    // elaborate on this later.
    public GameObject level1BackgroundElementPrefabs;
    public float spawnInterval;
    public float xRange = 10f; // how far left/right background elements can spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Repeatedly spawn background elements every spawn interval
        InvokeRepeating("Spawn", 0f, spawnInterval);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        int backgroundElementCount = GameObject.FindGameObjectsWithTag("BackgroundElement").Length;

        if (backgroundElementCount >= 15)
        {
            return;
        }

        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);
        Debug.Log("Spawn triggered at: " + Time.time);
        Instantiate(backgroundElement, spawnPosition, Quaternion.identity);
    }
}
