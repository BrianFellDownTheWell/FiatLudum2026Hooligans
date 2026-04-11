using UnityEngine;
using UnityEngine.UI;

public class MoveBalloon : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int health;
    private Vector2 velocity;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("Player health");
        Debug.Log(health);
        // Use left and right arrows, or the A and D keys to move
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){ 
            velocity = new Vector2(-1.0f, 0.0f) * Time.fixedDeltaTime * speed;
            rb.MovePosition(rb.position + velocity);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            velocity = new Vector2(1.0f, 0.0f) * Time.fixedDeltaTime * speed;
            rb.MovePosition(rb.position + velocity);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with Hazard");
        health -= 1;
    }

}
