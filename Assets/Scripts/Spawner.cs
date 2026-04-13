using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{

    public List<Sprite> hazardSprites;
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

        if (hazardCount >= 7)
        {
            return;
        }

        float randomX = Random.Range(-xRange, xRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y + 10f, 0);
        Debug.Log("Spawn triggered at: " + Time.time);
        // The instantiated hazard instance
        GameObject hazardInst = Instantiate(hazard, spawnPosition, Quaternion.identity);
        // Assign the instance to one of the possible sprite types
        if (hazardSprites.Count > 0)
        {
            SpriteRenderer sr = hazardInst.GetComponent<SpriteRenderer>();
            if (sr != null) 
            { 
                int spriteNum = Random.Range(0, hazardSprites.Count);
                sr.sprite = hazardSprites[spriteNum];
                sr.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            }
        }
    }
}
