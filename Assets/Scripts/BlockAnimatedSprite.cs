using UnityEngine;

public class BlockAnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite[] emptyBlockSprites;
    
    public float framerate = 1f/ 6f;

    private SpriteRenderer spriteRenderer;
    private BlockHit blockHit;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        blockHit = GetComponent<BlockHit>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Animate()
    {
        frame ++;
        if (blockHit.emptyBlock)
        {
            if (frame >= emptyBlockSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < emptyBlockSprites.Length)
            {
                spriteRenderer.sprite = emptyBlockSprites[frame];
            }
        }
        else
        {
            if (frame >= sprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < sprites.Length)
            {
                spriteRenderer.sprite = sprites[frame];
            }
        }
    }
}
