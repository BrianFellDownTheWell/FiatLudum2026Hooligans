using UnityEngine;
using UnityEngine.UI;

public class MoveBalloon : MonoBehaviour
{
    [SerializeField] private int speed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Use left and right arrows, or the A and D keys to move
        if (Input.GetKey(KeyCode.A) || Input.GetKeyKeyCode.LeftArrow)){
            velocity = new Vector2(-1.0f, 0.0f) * Time.fixedDeltaTime * speed;
            rb.MovePosition(rb.position + velocity);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            velocity = new Vector2(1.0f, 0.0f) * Time.fixedDeltaTime * speed;
            rb.MovePosition(rb.position + velocity);
        }
    }
}
