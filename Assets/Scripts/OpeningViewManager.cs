using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpeningViewManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelData
    {
        public GameObject balloonPrefab;
        public Sprite backgroundSprite;
        public string levelText;
    }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private Transform balloonSpawnPoint;
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private TMP_Text levelLabel;

    void Start()
    {
        int level = GameManager.Instance != null ? GameManager.Instance.CurrentLevel : 2;
        SetupLevel(level);
    }

    private void SetupLevel(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        LevelData data = levels[index];

        if (data.balloonPrefab != null)
        {
            Vector3 spawnPos = balloonSpawnPoint != null ? balloonSpawnPoint.position : Vector3.zero;
            Instantiate(data.balloonPrefab, spawnPos, Quaternion.identity);
        }

        if (backgroundRenderer != null && data.backgroundSprite != null)
            backgroundRenderer.sprite = data.backgroundSprite;

        if (levelLabel != null)
            levelLabel.text = data.levelText;
    }
}
