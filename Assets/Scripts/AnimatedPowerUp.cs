using UnityEngine;

public class AnimatedPowerUp : MonoBehaviour
{
    public Sprite[] mushSprites;
    public Sprite[] flowerSprites;
    
    public float framerate = 1f/ 6f;

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (GameManager.Instance.currentState > 0)
        {
            if (frame >= flowerSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < flowerSprites.Length)
            {
                spriteRenderer.sprite = flowerSprites[frame];
            }
        }
        else if (GameManager.Instance.currentState == 0)
        {
            if (frame >= mushSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < mushSprites.Length)
            {
                spriteRenderer.sprite = mushSprites[frame];
            }
        }
    }
}
