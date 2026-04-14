using UnityEngine;
using UnityEngine.UI;

public class ViewVisualsManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelVisuals
    {
        public Material quadMaterialA;
        public Material quadMaterialB;
        public Sprite levelImage;
    }

    [SerializeField] private LevelVisuals[] levels = new LevelVisuals[4];
    [SerializeField] private Renderer quadRendererA;
    [SerializeField] private Renderer quadRendererB;
    [SerializeField] private Image levelImageDisplay;

    void Start()
    {
        int level = GameManager.Instance != null ? GameManager.Instance.CurrentLevel : 1;
        SetupVisuals(level);
    }

    private void SetupVisuals(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        LevelVisuals data = levels[index];

        if (quadRendererA != null && data.quadMaterialA != null)
            quadRendererA.material = data.quadMaterialA;

        if (quadRendererB != null && data.quadMaterialB != null)
            quadRendererB.material = data.quadMaterialB;

        if (levelImageDisplay != null && data.levelImage != null)
            levelImageDisplay.sprite = data.levelImage;
    }
}
