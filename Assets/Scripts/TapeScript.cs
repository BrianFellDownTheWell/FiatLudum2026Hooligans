using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
    
    // initially this is true becuaase this guy is the source. otherwise when we drag it becomes a duplicate child guy
    private bool isTapeSource = true;
    private RectTransform currentDragGuy;
    private TapeScript activeDragScript;

    [SerializeField] public float timerVal = 40.0f;
    [SerializeField] public int targetPatches;
    private float currentTime;
    public bool isTimerEnabled = true;

    public AudioManager am;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Sprite[] dragSprites;

    private MinigameManager minigameManager;
    private int currentPatches;
    private void Awake()
    {
        currentPatches = 0;

        //SetTapeDifficulty();
        currentTime = timerVal;
        isTimerEnabled = true;

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        minigameManager = GetComponentInParent<MinigameManager>();
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

    private void Update()
    {
        if (!isTapeSource || !isTimerEnabled) return;

        currentTime -= Time.deltaTime;
        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();
        if (currentTime <= 0f)
        {
            isTimerEnabled = false;
            if (minigameManager != null)
                minigameManager.Lose();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragArea == null)
        {
            return;
        }


        if (isTapeSource)
        {
            // Create a duplicate of this tape
            GameObject duplicate = Instantiate(gameObject, dragArea);
            activeDragScript = duplicate.GetComponent<TapeScript>();
            currentDragGuy = duplicate.GetComponent<RectTransform>();

            activeDragScript.isTapeSource = false;
            activeDragScript.originalPosition = currentDragGuy.localPosition;
            activeDragScript.successZones = successZones;

            if (dragSprites != null && dragSprites.Length > 0)
            {
                Image img = duplicate.GetComponent<Image>();
                if (img != null) img.sprite = dragSprites[Random.Range(0, dragSprites.Length)];
            }

            currentDragGuy.SetAsLastSibling();
            
        }
        else
        {
            currentDragGuy = rectTransform;
            rectTransform.SetAsLastSibling();
        }
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragArea,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPosition))
        {
            pointerOffset = currentDragGuy.localPosition - (Vector3)localPointerPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragArea == null || currentDragGuy == null)
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
        currentDragGuy.localPosition = targetLocalPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        foreach (RectTransform zone in successZones)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(zone, eventData.position, eventData.pressEventCamera))
            {
                Vector3 zoneCenter = zone.localPosition;
                currentDragGuy.localPosition = zoneCenter;
                activeDragScript = null;
                currentDragGuy = null;
                currentPatches++;
                if(currentPatches >= targetPatches)
                {
                    isTimerEnabled = false;
                    if (minigameManager != null)
                        minigameManager.Win();
                }
                return;
            }
        }
        // If not snapped to any zone, destroy the duplicate
        if (currentDragGuy != null)
        {
            Destroy(currentDragGuy.gameObject);
        }
        activeDragScript = null;
        currentDragGuy = null;
    }
}
