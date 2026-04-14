using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Spawner : MonoBehaviour
{

    public List<Sprite> hazardSprites;
    public GameObject hazard;
    public float spawnInterval;
    public float xRange = 8f; // how far left/right hazards can spawn
    public float surviveTime = 30f;

    [SerializeField] private TMP_Text timerText;

    private float timer;
    private MinigameManager minigameManager;
    private bool finished;

    void Start()
    {
        minigameManager = GetComponentInParent<MinigameManager>();
        timer = surviveTime;
        UpdateTimerDisplay();
        InvokeRepeating("Spawn", 0f, spawnInterval);    
    }

    void Update()
    {
        if (finished) return;

        timer -= Time.deltaTime;
        timer = Mathf.Max(timer, 0f);
        UpdateTimerDisplay();

        if (timer <= 0f)
        {
            finished = true;
            CancelInvoke("Spawn");
            if (minigameManager != null)
                minigameManager.Win();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();
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
        GameObject hazardInst = Instantiate(hazard, spawnPosition, Quaternion.identity, transform.parent);
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
