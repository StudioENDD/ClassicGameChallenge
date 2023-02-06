using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{

    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;
    public int remainingTime;
    private Transform playerY;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerY = other.gameObject.transform;
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }   
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        GameManager.Instance.countDownRate = 0f;
        player.GetComponent<PlayerController>().enabled = false;
        Scoring();
        
        GameManager.Instance.playerClimb = true;
        yield return MoveTo(player, poleBottom.position);
        GameManager.Instance.playerClimb = false;
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);

        while (GameManager.Instance.timer > 0)
        {
            GameManager.Instance.timeValue -= 0.4f;
            GameManager.Instance.AddScore(50);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while(Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        subject.position = destination;
    }
    private void Scoring()
    {
        if (playerY.position.y >= 11)
        {
            GameManager.Instance.AddScore(5000);
        }
        else if (playerY.position.y >= 8.5)
        {
            GameManager.Instance.AddScore(2000);
        }
        else if (playerY.position.y >= 6.5)
        {
            GameManager.Instance.AddScore(800);
        }
        else if (playerY.position.y >= 4)
        {
            GameManager.Instance.AddScore(400);
        }
        else
        {
            GameManager.Instance.AddScore(100);
        }
    }
}
