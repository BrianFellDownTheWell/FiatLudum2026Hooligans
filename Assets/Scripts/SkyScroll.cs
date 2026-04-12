using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SkyScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;

    private Material materialInstance;
    private Vector2 textureOffset;

    private void Awake()
    {
        materialInstance = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        textureOffset.y = Mathf.Repeat(textureOffset.y - scrollSpeed * Time.deltaTime, 1f);
        materialInstance.SetTextureOffset("_BaseMap", textureOffset);
    }
}
