using UnityEngine;

public class Goomba : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        Destroy(gameObject, 0.5f);
        // Sprite Stuff would happen here too
    }
}