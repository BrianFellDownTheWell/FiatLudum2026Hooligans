using UnityEngine;

public class TeaObj : MonoBehaviour
{
    public TeaManager manager;
    private bool isDragging = false;
    private Vector3 offset;
    public string target = "Dropzone";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseDown()
    {
        // prevent object from snapping when dragged
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        CheckDrop();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 pt = Input.mousePosition;
        pt.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(pt);
    }

    void CheckDrop()
    {
        // See if the mouse was released over a specific zone
        Collider2D[] hitStats = Physics2D.OverlapPointAll(transform.position);

        foreach (var hitStat in hitStats)
        {
            if (hitStat != null && hitStat.gameObject != this.gameObject)
            {
                Debug.Log("Hit something");
                if (hitStat.CompareTag(target))
                {
                    // snap to the target zone
                    transform.position = hitStat.transform.position;
                    Debug.Log("Placed object in zone");

                    this.enabled = false;

                    manager.AddObj();
                }
            }
        }
    }
}
