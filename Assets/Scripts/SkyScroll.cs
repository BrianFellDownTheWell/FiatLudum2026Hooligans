using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SkyScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;

    private Renderer rend;
    private Vector2 textureOffset;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        textureOffset.y = Mathf.Repeat(textureOffset.y - scrollSpeed * Time.deltaTime, 1f);
        rend.material.SetTextureOffset("_BaseMap", textureOffset);
    }
}
