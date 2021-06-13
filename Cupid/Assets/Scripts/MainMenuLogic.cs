using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject main;
    public GameObject intro;

    MusicManager musicManager;

    void Awake()
    {
        musicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
    }

    void Start()
    {
        musicManager.StopMusic();
    }

    public void StartNewGame()
    {
        musicManager.StartMusic();
        SceneManager.LoadScene(1);
    }

    public void GoBack()
    {
        main.SetActive(true);
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
