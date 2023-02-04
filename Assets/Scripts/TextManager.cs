using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text coinsText;
    public TMP_Text worldText;
    public TMP_Text timeText;
    public TMP_Text livesText;


    void Start()
    {
        scoreText.text = "SCORE\n" + GameManager.Instance.score.ToString();
        coinsText.text = "COINS\n" + GameManager.Instance.coins.ToString();
        worldText.text = "WORLD\n" + GameManager.Instance.world.ToString() + "-" + GameManager.Instance.stage.ToString();
        timeText.text = "TIME\n" + GameManager.Instance.time.ToString();
        livesText.text = "LIVES\n" + GameManager.Instance.lives.ToString();
    }

    void Update()
    {
        scoreText.text = "SCORE\n" + GameManager.Instance.score.ToString();
        coinsText.text = "COINS\n" + GameManager.Instance.coins.ToString();
        worldText.text = "WORLD\n" + GameManager.Instance.world.ToString() + "-" + GameManager.Instance.stage.ToString();
        timeText.text = "TIME\n" + GameManager.Instance.time.ToString();
        livesText.text = "LIVES\n" + GameManager.Instance.lives.ToString();
    }
}
