using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public GameObject heartsContainer;
    public GameObject heartPrefab;
    public GameObject brokenHeartPrefab;

    List<GameObject> hearts = new List<GameObject>();

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void InitialiseHearts(int numHearts)
    {
        for (int i = 0; i < numHearts; ++i)
        {
            hearts.Add(Instantiate(heartPrefab, heartsContainer.transform));
        }
    }

    public void BreakHeart()
    {
        if (hearts.Count > 0)
        {
            Destroy(hearts[0]);
            hearts.RemoveAt(0);
            hearts.Add(Instantiate(brokenHeartPrefab, heartsContainer.transform));
        }
    }
}
