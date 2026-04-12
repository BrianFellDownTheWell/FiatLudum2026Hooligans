using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class TapeScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // canvas we're wroking on
    private Canvas canvas;
    // rect transform of the tape
    private RectTransform rectTransform;
    // area we can drag within (parent of the tape)
    private RectTransform dragArea;
    // canvas group
    private CanvasGroup canvasGroup;
    // to add to new position of tape 
    private Vector2 pointerOffset;
    // original position just in case dropped in a bad area
    private Vector3 originalPosition;

    [SerializeField] private List<RectTransform> successZones = new List<RectTransform>();


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        dragArea = rectTransform.parent as RectTransform;
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.localPosition;

        // Gather successZones
        if (canvas != null)
        {
            // loop to get children of parent canvas
            foreach (Transform child in canvas.transform)
            {
                if (child.name.StartsWith("Zone") && child.GetComponent<RectTransform>() != rectTransform)
                {
                    // Add to successZones list
                    successZones.Add(child.GetComponent<RectTransform>());
                }
            }
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        if (canvas == null)
        {
            Debug.LogError("TapeScript requires this UI element to be placed under a Canvas.", this);
            enabled = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragArea == null)
        {
            return;
        }

        rectTransform.SetAsLastSibling();
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragArea,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPosition))
        {
            pointerOffset = rectTransform.localPosition - (Vector3)localPointerPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragArea == null)
        {
            return;
        }

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragArea,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPosition))
        {
            return;
        }

        Vector3 targetLocalPosition = localPointerPosition + pointerOffset;
        rectTransform.localPosition = targetLocalPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        foreach (RectTransform zone in successZones)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(zone, eventData.position, eventData.pressEventCamera))
            {
                Vector3 zoneCenter = zone.localPosition;
                rectTransform.localPosition = zoneCenter;
                return;
            }
        }
        // If not snapped to any zone, return to original position
        rectTransform.localPosition = originalPosition;
    }
}
