using UnityEngine;

public class FireballAnimation : MonoBehaviour
{
    public Sprite[] fireballSprites;
    public Sprite[] explosionSprites;
    
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
            if (frame >= fireballSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < fireballSprites.Length)
            {
                spriteRenderer.sprite = fireballSprites[frame];
            }
        }
        else if (GameManager.Instance.currentState == 0)
        {
            if (frame >= explosionSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < explosionSprites.Length)
            {
                spriteRenderer.sprite = explosionSprites[frame];
            }
        }
    }
}
