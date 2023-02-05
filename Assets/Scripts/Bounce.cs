using UnityEngine;

public class Bounce : MonoBehaviour
{
    private new BoxCollider2D collider;
    public bool hit;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        hit = false;
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            hit = true;
        }
    }
}
