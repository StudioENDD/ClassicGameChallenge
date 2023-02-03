using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite deadSprite;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }
    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        if (deadSprite != null) 
        {
            spriteRenderer.sprite = deadSprite;
        }
    }
    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerController playerController = GetComponent<PlayerController>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
        if (entityMovement != null)
        {
            entityMovement.enabled = false;
        }
    }
    private IEnumerator Animate()
    {
        float elapsed = 0;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -36;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
