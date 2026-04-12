using UnityEngine;

public class TeaManager : MonoBehaviour
{
    [SerializeField] public int targetObjCount;

    private int currentObjCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddObj()
    {
        currentObjCount++;
        if (currentObjCount >= targetObjCount) {
            Debug.Log("Completed tea minigame");
        }
    }
}
