using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public GameObject credits;
    public GameObject controls;


    public void PlayToGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
        
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenControls()
    {
        controls.SetActive(true);
    }

    public void closeControls()
    {
        controls.SetActive(false);
    }
}
