using UnityEngine;

public class PlayerAnimatedSprite : MonoBehaviour
{
    public Sprite[] crouchSpritesL;
    public Sprite[] crouchSpritesR;
    public Sprite[] idleSpritesL;
    public Sprite[] idleSpritesR;
    public Sprite[] jumpSpritesL;
    public Sprite[] jumpSpritesR;
    public Sprite[] slideSpritesL;
    public Sprite[] slideSpritesR;
    public Sprite[] runSpritesL;
    public Sprite[] runSpritesR;
    public Sprite[] throwSpritesL;
    public Sprite[] throwSpritesR;
    public Sprite[] poleSprite;
    public Sprite[] deathSprite;
    public float framerate = 1f/ 6f;

    private SpriteRenderer spriteRenderer;
    private PlayerController movement;
    private DeathAnimation deathAnimation;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<PlayerController>();
        deathAnimation = GetComponentInParent<DeathAnimation>();
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

        if (deathAnimation.dead)
        {
            if (frame >= deathSprite.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < deathSprite.Length)
            {
                spriteRenderer.sprite = deathSprite[frame];
            }
        }
        else if (GameManager.Instance.playerClimb)
        {
            if (frame >= poleSprite.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < poleSprite.Length)
            {
                spriteRenderer.sprite = poleSprite[frame];
            }
        }
        else if (!movement.faceLeft)
        {
            if (crouchSpritesR.Length != 0 && movement.crouching)
            {
                if (frame >= crouchSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < crouchSpritesR.Length)
                {
                    spriteRenderer.sprite = crouchSpritesR[frame];
                }
            }
            else if (throwSpritesR.Length != 0 && movement.throwing)
            {
                if (frame >= throwSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < throwSpritesR.Length)
                {
                    spriteRenderer.sprite = throwSpritesR[frame];
                }
            }
            else if (movement.jumping)
            {
                if (frame >= jumpSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < jumpSpritesR.Length)
                {
                    spriteRenderer.sprite = jumpSpritesR[frame];
                }
            }
            else if (movement.sliding)
            {
                if (frame >= slideSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < slideSpritesR.Length)
                {
                    spriteRenderer.sprite = slideSpritesR[frame];
                }
            }
            else if (movement.running)
            {
                if (frame >= runSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < runSpritesR.Length)
                {
                    spriteRenderer.sprite = runSpritesR[frame];
                }
            }
            else
            {
                if (frame >= idleSpritesR.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < idleSpritesR.Length)
                {
                    spriteRenderer.sprite = idleSpritesR[frame];
                }
            }
        }
        else
        {
            if (crouchSpritesL.Length != 0 && movement.crouching)
            {
                if (frame >= crouchSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < crouchSpritesL.Length)
                {
                    spriteRenderer.sprite = crouchSpritesL[frame];
                }
            }
            else if (throwSpritesL.Length != 0 && movement.throwing)
            {
                if (frame >= throwSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < throwSpritesL.Length)
                {
                    spriteRenderer.sprite = throwSpritesL[frame];
                }
            }
            else if (movement.jumping)
            {
                if (frame >= jumpSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < jumpSpritesL.Length)
                {
                    spriteRenderer.sprite = jumpSpritesL[frame];
                }
            }
            else if (movement.sliding)
            {
                if (frame >= slideSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < slideSpritesL.Length)
                {
                    spriteRenderer.sprite = slideSpritesL[frame];
                }
            }
            else if (movement.running)
            {
                if (frame >= runSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < runSpritesL.Length)
                {
                    spriteRenderer.sprite = runSpritesL[frame];
                }
            }
            else
            {
                if (frame >= idleSpritesL.Length)
                {
                    frame = 0;
                }

                if (frame >= 0 && frame < idleSpritesL.Length)
                {
                    spriteRenderer.sprite = idleSpritesL[frame];
                }
            }
        }
    }
}
