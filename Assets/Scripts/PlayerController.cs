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
    public Transform groundChecker;
    private Vector2 velocity;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        //This checks if the ground is within 0.1 units of the bottom of the player
        isGrounded = Physics2D.OverlapCircle(groundChecker.transform.position, 0.1f, groundLayer);
        
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
        /*Vector2 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, accelerationSpeed * Time.deltaTime);
        */
        velocity.x = Mathf.MoveTowards(velocity.x, moveX * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void GroundMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
        if (Input.GetButtonDown("Jump"))
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
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
}