using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject title;
    public GameObject startGameButton;
    public GameObject creditsButton;
    public GameObject names;
    public GameObject backbutton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        title.SetActive(false);
        startGameButton.SetActive(false);
        creditsButton.SetActive(false);
        names.SetActive(true);
        backbutton.SetActive(true);
    }

    public void GoBack()
    {
        title.SetActive(true);
        startGameButton.SetActive(true);
        creditsButton.SetActive(true);
        names.SetActive(false);
        backbutton.SetActive(false);
    }
}
