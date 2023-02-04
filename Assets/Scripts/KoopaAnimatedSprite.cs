using UnityEngine;

public class KoopaAnimatedSprite : MonoBehaviour
{
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    public Sprite[] shellSprites;
    
    public float framerate = 1f/ 6f;

    private SpriteRenderer spriteRenderer;
    private Koopa koopa;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        koopa = GetComponent<Koopa>();
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
        
        if (koopa.shelled)
        {
            if (frame >= shellSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < shellSprites.Length)
            {
                spriteRenderer.sprite = shellSprites[frame];
            }
        }
        else if (!koopa.faceLeft)
        {
            if (frame >= rightSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < rightSprites.Length)
            {
                spriteRenderer.sprite = rightSprites[frame];
            }
        }
        else if (koopa.faceLeft)
        {
            if (frame >= leftSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < leftSprites.Length)
            {
                spriteRenderer.sprite = leftSprites[frame];
            }
        }

        

    }
}
