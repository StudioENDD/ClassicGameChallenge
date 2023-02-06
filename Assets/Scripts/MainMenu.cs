using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public GameObject main;
    public GameObject credits;
    public GameObject controls; 
    private AudioManager audioManager;
    public GameObject audio;
    public bool winScreen;

    private void Awake()
    {
        audioManager = audio.GetComponent<AudioManager>();
        main.SetActive(true);
        credits.SetActive(false);
        controls.SetActive(false);
    }

    private void Start()
    {
        main.SetActive(true);
        credits.SetActive(false);
        controls.SetActive(false);
        Debug.Log("Play Theme");
        audioManager.Play("MainTheme");
    }

    private void update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void PlayGame()
    {
        ClickSound();
        audioManager.Stop("MainTheme");
        SceneManager.LoadScene("Stage 1");

    }

    public void SeeCreditsMenu()
    {
        ClickSound();
        main.SetActive(false);
        credits.SetActive(true);
        controls.SetActive(false);
    }

    public void SeeControlMenu()
    {
        ClickSound();
        main.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(true);
    }

    public void Back()
    {
        if (!winScreen)
        {
            ClickSound();
            main.SetActive(true);
            credits.SetActive(false);
            controls.SetActive(false);   
        }
        else if (winScreen)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    
    public void Win()
    {
        SceneManager.LoadScene("Win Screen");
    }
    
    public void QuitGame()
    {
        ClickSound();
        Debug.Log("quit");
        Application.Quit();
    }

    public void ClickSound()
    {
        
            audioManager.Play("ClickSound");
        
    }
}
