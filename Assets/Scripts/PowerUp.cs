using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starpower,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Coin:
            GameManager.Instance.AddCoin();
            GameManager.Instance.AddScore(200);
            break;

            case Type.ExtraLife:
            GameManager.Instance.AddLife();
            GameManager.Instance.AddScore(1000);
            break;

            case Type.MagicMushroom:
            player.GetComponent<Player>().Grow();
            GameManager.Instance.AddScore(1000);
            break;

            case Type.Starpower:
            player.GetComponent<Player>().StarPower();
            GameManager.Instance.AddScore(1000);
            break;
        }

        Destroy(gameObject);
    }
}
