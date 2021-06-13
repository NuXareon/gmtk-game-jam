using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulationsLogic : MonoBehaviour
{
    IEnumerator returnToMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        returnToMainMenu = GoBackToMainMenu();
        StartCoroutine(returnToMainMenu); 
    }

    IEnumerator GoBackToMainMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
