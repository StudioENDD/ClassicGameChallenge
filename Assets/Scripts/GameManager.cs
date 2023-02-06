using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world {get; private set; }
    public int stage {get; private set; }
    public int lives {get; private set; }
    public int coins {get; private set; }
    public int score {get; private set; }
    public int timer;
    public float timeValue;
    public float countDownRate;
    public int currentState;
    public bool playerFaceLeft;
    public int maxFireballs;
    public int currentFireballs;
    public bool playerClimb;
    private int persScore;
    

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
        countDownRate = Time.deltaTime;
        Instance.maxFireballs = 2;
        Instance.currentFireballs = 0;
        playerClimb = false;
        stage = 1;
        world = 1;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (timeValue > 0)
        {
            timeValue -= countDownRate;
        }
        else if (timeValue <= 0)
        {
            timeValue = 0;
        }
        timer = (int) (timeValue * 2.5);

        if (currentFireballs < 0)
        {
            currentFireballs = 0;
        }
      
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        score = 0;
        timer = 400;
        timeValue = 160;
        LoadLevel(1, 1);
    }

    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        if (stage == this.stage)
        {
            score = 0;
            timer = 400;
            timeValue = 160;
        }
        else
        {
            persScore = 0;
            timer = 400;
            timeValue = 160;
        }


        SceneManager.LoadScene($"Stage " + stage);
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
            SceneManager.LoadScene("Game Over"); 
            lives = 3;
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

    public void AddScore(int Score)
    {
        score += Score;
    }
}