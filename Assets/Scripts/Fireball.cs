using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Rigidbody2D rb;
    private new Collider2D collider;
    private Bounce bounce;

    private Vector2 velocity;
    public Vector2 direction = Vector2.left;
    public bool hit;

    public float speed = 5f;
    private float bounceHeight = 1f;
    private float bounceTime = 0.5f;
    public float bouncePower => (2f * bounceHeight) / (bounceTime / 2f);
    public float gravity => (-2f * bounceHeight) / Mathf.Pow((bounceTime / 2f), 2);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        bounce = GetComponent<Bounce>();
        velocity = Vector2.zero;
        rb.isKinematic = false;
        collider.enabled = true;
        hit = false;
    }

    private void Update()
    {
        if (bounce.hit)
        {
            hit = true;
        }
        else if (!hit)
        {
            AddGravity();
        }

        if (hit)
        {
            StartCoroutine(BlowUp());
        }
        
    }

    private void AddGravity()
    {
        bool falling = velocity.y < 0f;
        float multiplier = falling ? 2f : 1f;
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnBecameInvisible()
    {
        StartCoroutine(BlowUp());
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        velocity.x = direction.x * speed;
        position += velocity * Time.fixedDeltaTime;
        rb.MovePosition(position);
    }

    public IEnumerator BlowUp()
    {
        Debug.Log("BlowUP");
        hit = true;
        rb.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        direction = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.currentFireballs --;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            velocity.y = bouncePower;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(BlowUp());
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            StartCoroutine(BlowUp());
        }
    }
}
