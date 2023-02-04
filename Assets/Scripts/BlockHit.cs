using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    public GameObject fireFlower;
    public Sprite emptyBlock;
    public bool emptyBlockActive;
    public int maxHits = -1;
    private bool animating;
    public bool breakable = false;
    //private float blockSpeed = 0.5f;
    private AnimatedSprite animatedSprite;
    private Player player;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!animating && maxHits != 0 && col.gameObject.CompareTag("Player"))
        {
            if(col.transform.DotTest(transform, Vector2.up))
            {
                player = col.gameObject.GetComponent<Player>();
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        animatedSprite = GetComponent<AnimatedSprite>();
        spriteRenderer.enabled = true;
        
        
        if (!breakable)
        {
            maxHits --;

            if (maxHits == 0) 
            {
                animatedSprite.enabled = false;
                spriteRenderer.sprite = emptyBlock;
            }

            if (item != null)
            {
                Instantiate(item, transform.position, Quaternion.identity);
            }

            StartCoroutine(Animate());
        }
        else if (breakable && !player.small)
        {
            //StartCoroutine(BreakAnimate());
            GameManager.Instance.AddScore(50);
            Destroy(gameObject);
        }
        else if (breakable && player.small)
        {
            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        animating = true;
        
        Vector3 restingPos = transform.localPosition;
        Vector3 animatedPos = restingPos + Vector3.up * 0.5f;

        yield return Move(restingPos, animatedPos);
        yield return Move(animatedPos, restingPos);

        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed /duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;
    }

    private IEnumerator BreakAnimate()
    {
        // TODO
        yield return null;
    }
}
