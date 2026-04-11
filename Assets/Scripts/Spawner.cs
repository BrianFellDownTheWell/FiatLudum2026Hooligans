using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject hazard;
    public float spawnInterval;
    public float xRange = 8f; // how far left/right hazards can spawn

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Repeatedly spawn enemies every spawn interval
        InvokeRepeating("Spawn", 0f, spawnInterval);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        int hazardCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (hazardCount >= 15)
        {
            return;
        }

        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);
        Debug.Log("Spawn triggered at: " + Time.time);
        Instantiate(hazard, spawnPosition, Quaternion.identity);
    }
}
