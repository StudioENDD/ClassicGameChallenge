using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float shellSpeed = 12;

    public bool shelled;
    private bool pushed;
    public bool faceLeft;
    private EntityMovement movement;

    private void Awake()
    {
        movement = GetComponent<EntityMovement>();
    }
    private void Update()
    {
        if(movement.direction.x < 0)
        {
            faceLeft = true;
        }
        else if (movement.direction.x > 0)
        {
            faceLeft = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!shelled && col.gameObject.CompareTag("Player"))
        {
            Player player = col.gameObject.GetComponent<Player>();

            if (player.starPower)
            {
                GameManager.Instance.AddScore(100);
                Hit();
            }
            else if (col.transform.DotTest(transform, Vector2.down))
            {
                GameManager.Instance.AddScore(100);
                EnterShell();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (shelled && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                GameManager.Instance.AddScore(400);
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starPower)
                {
                    Hit();
                }
                else
                {
                    player.Hit();
                }
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            GameManager.Instance.AddScore(500);
            Hit();
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            GameManager.Instance.AddScore(200);
            Hit();
            if(other != null)
            {
                Fireball fireball = other.gameObject.GetComponent<Fireball>(); 
                StartCoroutine(fireball.BlowUp());
            }
        }
        else if (!shelled && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            movement.direction = -movement.direction;
        }
    }
    private void EnterShell()
    {
        shelled = true;
        GetComponent<EntityMovement>().enabled = false;
        //GetComponent<KoopaAnimatedSprite>().enabled = false;
    }

    private void PushShell(Vector2 direction)
    {
        pushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = shellSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<KoopaAnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed)
        {
            Destroy(gameObject);
        }
    }
}