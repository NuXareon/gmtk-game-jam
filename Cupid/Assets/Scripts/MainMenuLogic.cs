using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject main;
    public GameObject intro;
    public GameObject credits;

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        main.SetActive(false);
        credits.SetActive(true);
    }

    public void GoBack()
    {
        main.SetActive(true);
        credits.SetActive(false);
        intro.SetActive(false);
    }
    public void OpenIntro()
    {
        main.SetActive(false);
        intro.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
