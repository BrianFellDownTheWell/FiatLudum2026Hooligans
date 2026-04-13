using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveBalloon : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int health;
    private Vector2 velocity;
    private Rigidbody2D rb;
    [SerializeField] private float timerVal = 60.0f;

    private float currentTime;

    public AudioManager man;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTime = timerVal;
    }

    void Update()
    {
        Debug.Log("timer value");
        Debug.Log(currentTime);

        if (currentTime <= 0)
        {
            Debug.Log("Time's up! Completed dodge obstacles minigame");
        }

        if (health <= 0) {
            Debug.Log("U R DED");
            SceneManager.LoadScene("FallEnding");
        }

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

        // This is just to test the level advancing; remove this when done
        if (Input.GetKey(KeyCode.K))
        {
            man.UpdateMusic(2);
        }

        currentTime -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with Hazard");
        health -= 1;
    }

}
