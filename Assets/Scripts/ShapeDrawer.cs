using UnityEngine;
using System.Collections.Generic;

public class ShapeDrawer : MonoBehaviour
{

    private LineRenderer ren;
    private List<Vector3> points = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ren = GetComponent<LineRenderer>();
        // empty line
        ren.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseLoc = GetWorldMousePosition();
            Debug.Log("Mouse location: " + mouseLoc);

            Collider2D hit = Physics2D.OverlapPoint(mouseLoc);
            if (hit != null)
            {
                Debug.Log("Hit something");
                if (hit.CompareTag("Checkpoint"))
                {
                    Debug.Log("Contacting squiggle manager because of hit");
                    // If a checkpoint is touched, inform manager
                    FindFirstObjectByType<SquiggleManager>().OnCheckpointHit(hit.gameObject);
                    AddPoint(hit.transform.position);
                }
            }
        }
    }

    public void AddPoint(Vector3 punto)
    {
        points.Add(punto);
        ren.positionCount = points.Count;
        ren.SetPosition(points.Count - 1, punto);
    }

    Vector3 GetWorldMousePosition()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(pos);
        worldPosition.z = 0f;
        return worldPosition;
    }

// Draw 
    void OnDrawGizmos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mousePos, 0.2f);
    }
}
