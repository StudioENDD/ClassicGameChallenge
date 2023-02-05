using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;
    public bool exit;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode)) 
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerController>().enabled = false;
        
        Vector3 enteredPos = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPos, enteredScale);
        yield return new WaitForSeconds(0f);

        bool underground = connection.position.y < 0f;
        Camera.main.GetComponent<CameraFollow>().SetUnderGround(underground, connection);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerController>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPos, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPos = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPos, endPos, t);
            if(exit)
            {
                player.localScale = Vector3. Lerp(startScale, endScale, t);

            }
            //            
            elapsed += Time.deltaTime;

            yield return null;
        }

        if(!exit)
        {
            //Camera.main.GetComponent<CameraFollow>().Transform.position.x = endPos.x + 15.5f;
            player.position = endPos;
            player.localScale = endScale;
        }
        else
        {
            //Camera.main.GetComponent<CameraFollow>().Transform.position.x = endPos.x + 15.5f;
            player.position = endPos;
            endPos = player.position;
            startPos = player.position;
            endPos.x += 2;
            while (elapsed < duration)
            {
                float t = elapsed / duration;

                player.position = Vector3.Lerp(startPos, endPos, t);
                //player.localScale = Vector3. Lerp(startScale, endScale, t);
                elapsed += Time.deltaTime;

                yield return null;
            }
        }

        
    }
}
