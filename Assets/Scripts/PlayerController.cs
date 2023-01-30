using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    //public float accelerationSpeed = 2f;
    //public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public LayerMask groundLayer;
    private Vector2 velocity;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    
    [SerializeField] private Transform groundChecker;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //This checks if the ground is within 0.1 units of the bottom of the player
        Vector2 boxHalfExtent = new Vector2(0.25f, 0.05f);
        isGrounded = Physics2D.OverlapBox(groundChecker.transform.position, boxHalfExtent, 0, groundLayer);
        HorizontalMovement();
        
        if (isGrounded)
        {
            GroundMovement();
        }

        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, moveX * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void GroundMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }
    }

    private void ApplyGravity()
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

        rb.MovePosition(position);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            //velocity.y = 0f;
        }
    }*/
}