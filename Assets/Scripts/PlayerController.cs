using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private new Collider2D collider;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPos;
    public int maxFireballs = 2;
    public int fireballCount = 0;
    
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
    public bool crouching { get; private set; }
    public bool throwing { get; private set; }
    public bool faceLeft { get; private set; }
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        cam = Camera.main;
        maxFireballs = 2;
        fireballCount = 0;
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
        crouching = false;
        faceLeft = false;
        throwing = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
        crouching = false;
        throwing = false;
    }

    private void Update()
    {
        SideMovement();
        isGrounded = rb.Raycast(Vector2.down);
        GameManager.Instance.playerFaceLeft = faceLeft;

        if(isGrounded && Input.GetKey(KeyCode.S))
        {   
            crouching = true;
        }
        else if (crouching && Input.GetKey(KeyCode.S))
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
        
        if (isGrounded) 
        {
            GroundMovement();
        }
        AddGravity();

        jumping = !isGrounded;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;

            if (Input.GetKeyDown(KeyCode.LeftShift) && fireballCount < maxFireballs && GameManager.Instance.currentState == 2)
            {
                StartCoroutine(ThrowFire());
            }
            else
            {
                throwing = false;
            }
        }
        else
        {
            moveSpeed = 8f;
        }
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
            //transform.eulerAngles = Vector3.zero;
            faceLeft = false;
        } 
        else if (velocity.x < 0f) 
        {
            //transform.eulerAngles = new Vector3(0f, 180f);
            faceLeft = true;
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
        velocity.y = Mathf.Max(velocity.y, gravity / 4f);
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

    public IEnumerator ThrowFire()
    {
        throwing = true;
        fireballCount ++;
        Instantiate(fireballPrefab, fireballSpawnPos.position, Quaternion.identity);
        
        yield return new WaitForSeconds(0.25f);
        throwing = false;
        yield return null;
    }
}