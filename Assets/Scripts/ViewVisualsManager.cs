using UnityEngine;

public class ViewVisualsManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelVisuals
    {
        public Material quadMaterialA;
        public Material quadMaterialB;
    }

    [SerializeField] private LevelVisuals[] levels = new LevelVisuals[4];
    [SerializeField] private Renderer quadRendererA;
    [SerializeField] private Renderer quadRendererB;

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
    }
}
