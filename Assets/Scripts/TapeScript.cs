using UnityEngine;
using UnityEngine.EventSystems;

public class TapeScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Tape Pressed");
    }


    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Tape Released");
    }

}
