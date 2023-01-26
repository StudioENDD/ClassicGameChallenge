using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement speed
    public float moveSpeed = 5f;
    //Jump force
    public float jumpForce = 5f;
    //Rigidbody component
    private Rigidbody2D rb;
    //Boolean to check if player is on the ground
    private bool isGrounded;
    //Raycast origin
    public Transform groundCheck;
    //Raycast direction
    public Vector2 groundCheckDirection = Vector2.down;
    //Raycast distance
    public float groundCheckDistance = 0.2f;
    //Layer mask for ground
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is on the ground
        isGrounded = Physics2D.Raycast(groundCheck.position, groundCheckDirection, groundCheckDistance, whatIsGround);

        //Move player horizontally
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        //Make player jump if space key is pressed and player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}