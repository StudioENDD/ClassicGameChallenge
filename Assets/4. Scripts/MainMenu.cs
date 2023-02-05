using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void SeeCreditsMenu()
    {
        SceneManager.LoadScene("Credits Menu");
    }

    public void SeeControlMenu()
    {
        SceneManager.LoadScene("Control Menu");
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
