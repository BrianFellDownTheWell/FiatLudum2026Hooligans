using UnityEngine;
using UnityEngine.EventSystems;

public class CheckpointClick : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("Clicked UI sphere: " + gameObject.name);

        // send info
        FindFirstObjectByType<SquiggleManager>().OnCheckpointHit(gameObject);
        FindFirstObjectByType<ShapeDrawer>().AddPoint(transform.position);
    }
}
