using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world {get; private set; }
    public int stage {get; private set; }
    public int lives {get; private set; }
    public int coins {get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            DestroyImmediate(gameObject);
        } 
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) 
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene("Stage 1");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void PlayerDeath(float delay)
    {
        Invoke(nameof(PlayerDeath), delay);
    }

    public void PlayerDeath()
    {
        lives --;

        if (lives > 0) 
        {
            LoadLevel(world, stage);
        } 
        else
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        NewGame();
    }

    public void AddCoin()
    {
        coins ++;

        if (coins == 100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()
    {
        lives++;
    }
}