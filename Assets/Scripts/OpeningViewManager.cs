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
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text levelLabel;

    void Start()
    {
        int level = GameManager.Instance != null ? GameManager.Instance.CurrentLevel : 2;

        if (level < 2 || level > 4)
        {
            Debug.LogWarning("OpeningViewManager: unexpected level " + level + ", clamping to 2-4.");
            level = Mathf.Clamp(level, 2, 4);
        }

        SetupLevel(level);
    }

    private void SetupLevel(int level)
    {
        // levels array: index 0 = level 2, index 1 = level 3, index 2 = level 4
        int index = Mathf.Clamp(level - 2, 0, levels.Length - 1);
        LevelData data = levels[index];

        if (data.balloonPrefab != null)
        {
            Vector3 spawnPos = balloonSpawnPoint != null ? balloonSpawnPoint.position : Vector3.zero;
            Instantiate(data.balloonPrefab, spawnPos, Quaternion.identity);
        }

        if (backgroundImage != null && data.backgroundSprite != null)
            backgroundImage.sprite = data.backgroundSprite;

        if (levelLabel != null)
            levelLabel.text = data.levelText;

        if (level == 4 && levelLabel != null)
            levelLabel.color = Color.white;
    }
}
