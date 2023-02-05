using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private EntityMovement movement;

    private void Awake()
    {
        movement = GetComponent<EntityMovement>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
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
                Flatten();
            }
            else
            {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            GameManager.Instance.AddScore(500);
            Hit();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            GameManager.Instance.AddScore(100);
            Hit();
            if(other != null)
            {
                Fireball fireball = other.gameObject.GetComponent<Fireball>(); 
                StartCoroutine(fireball.BlowUp());
            }
            
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            movement.direction = -movement.direction;
        }
    }
    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}