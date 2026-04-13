using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    private float currentTime;
    public bool isTimerEnabled = true;

    public AudioManager am;

    private void Awake()
    {
        SetTapeDifficulty();
        currentTime = timerVal;
        isTimerEnabled = true;

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


        if (isTapeSource)
        {
            // Create a duplicate of this tape
            GameObject duplicate = Instantiate(gameObject, dragArea);
            activeDragScript = duplicate.GetComponent<TapeScript>();
            currentDragGuy = duplicate.GetComponent<RectTransform>();

            activeDragScript.isTapeSource = false;
            activeDragScript.originalPosition = currentDragGuy.localPosition;
            activeDragScript.successZones = successZones;
            activeDragScript.isTimerEnabled = false;
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
            if (RectTransformUtility.RectangleContainsScreenPoint(zone, RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, currentDragGuy.position), eventData.pressEventCamera))
            {
                //Vector3 zoneCenter = zone.localPosition;
                //currentDragGuy.localPosition = zoneCenter;
                currentDragGuy.position = zone.position;
                Debug.Log("Stuck onto zone: " + zone.name);
                activeDragScript = null;
                currentDragGuy = null;
                return;
            }
        }
        // If not snapped to any zone, return to original position

        if (!isTapeSource)
        {
            Destroy(currentDragGuy.gameObject);
        }
        else
        {
            currentDragGuy.localPosition = originalPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerEnabled)
        {
            return;
        }
        Debug.Log("timer value");
        Debug.Log(currentTime);

        if (currentTime <= 0)
        {
            Debug.Log("Time's up!");
            SceneManager.LoadScene("GameOver");
        }

        currentTime -= Time.deltaTime;
    }

    public void SetTapeDifficulty()
    {
        // Get the level from the audio manager
        int level = am.getLevel();

        // Set the difficulty for the tape depending on the level
        if (level == 1)
        {
            timerVal = 20.0f;
        }
        if (level == 2)
        {
            timerVal = 30.0f;
        }
        if (level == 3)
        {
            timerVal = 40.0f;
        }

        Debug.Log("Tape timer: " + timerVal);
    }
}
