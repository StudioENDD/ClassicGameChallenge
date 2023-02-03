using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerController movement;

    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Sprite run;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerController>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        //run.enabled = false;
    }

    private void LateUpdate()
    {
        // run.enabled = movement.running;
        if (movement.jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (movement.running)
        {
            spriteRenderer.sprite = run;
        }
        else if (!movement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
