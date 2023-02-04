using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody2D rb;
    private new Collider2D collider;
    public bool grounded;
    private Vector2 velocity;
    
    public float maxJumpHeight = 2f;
    public float maxJumpTime = 0.5f;
    public float jumpPower => (2f * maxJumpHeight) / (maxJumpTime / 2f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        grounded = rb.Raycast(Vector2.down);
        if(grounded)
        {
            velocity.y = jumpPower;
        }
    }
}
