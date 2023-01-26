using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float accelerationSpeed = 0.1f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public Vector2 groundCheckDirection = Vector2.down;
    public float groundCheckDistance = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, groundCheckDirection, groundCheckDistance, whatIsGround);

        float moveX = Input.GetAxis("Horizontal");
        Vector2 targetVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, accelerationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}