using UnityEngine;

public class Star : MonoBehaviour
{
    private Rigidbody2D rb;
    private new Collider2D collider;
    private Vector2 velocity;
    public Vector2 direction = Vector2.left;

    public float speed = 5f;
    public float bounceHeight = 1f;
    public float bounceTime = 0.5f;
    public float bouncePower => (2f * bounceHeight) / (bounceTime / 2f);
    public float gravity => (-2f * bounceHeight) / Mathf.Pow((bounceTime / 2f), 2);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        AddGravity();
    }

    private void AddGravity()
    {
        bool falling = velocity.y < 0f;
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rb.WakeUp();
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
        rb.Sleep();
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        velocity.x = direction.x * speed;
        position += velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);

        if (rb.Raycast(direction))
            {
                direction = -direction;
            }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            velocity.y = bouncePower;
        }
    }
}
