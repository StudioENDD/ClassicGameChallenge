using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private new Collider2D collider;
    
    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpPower => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool isGrounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        cam = Camera.main;
    }

    private void OnEnble()
    {
        rb.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        SideMovement();
        isGrounded = rb.Raycast(Vector2.down);
        if (isGrounded) 
        {
            GroundMovement();
        }
        AddGravity();
    }

    private void SideMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rb.Raycast(Vector2.right * velocity.x)) 
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        } 
        else if (velocity.x < 0f) 
        {
            transform.eulerAngles = new Vector3(0f, 180f);
        }
    }

    private void GroundMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpPower;
            jumping = true;
        }
    }

    private void AddGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = cam.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = cam.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        if (position.x <= leftEdge.x + 0.5f)
        {
            velocity.x = 0f;
        }

        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotTest(col.transform, Vector2.down))
            {
                velocity.y = jumpPower / 2f;
                jumping = true;
            }
        }
        if (col.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(col.transform, Vector2.up))
            {
                velocity.y = 0f;
                
            }
        }
    }
}