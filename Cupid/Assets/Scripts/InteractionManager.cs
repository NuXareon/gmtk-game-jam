using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{
    struct Couple
    {
        public GameObject first;
        public GameObject second;
    }

    public GameObject heartPrefab;
    public UnityEngine.UI.Text UILinksText;

    public static float defaultInteractionSpeed = 0.05f;
    public static int interactionLayer = 6;

    List<Couple> currentCouples = new List<Couple>();
    int playerCount = 0;
    int peopleCount = 0;
    int numLinks = 0;

    void Awake()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        playerCount = targets.Length;
        InteractionComponent[] people = GameObject.FindObjectsOfType<InteractionComponent>();
        peopleCount = people.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        UILinksText.text = "Links: " + numLinks.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount == 0)
        {
            Debug.Log("The power of love wins once again!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (playerCount < 0)
        {
            Debug.LogError("Invalid number of players left");
        }
        else if (playerCount == 1 && peopleCount == 1)
        {
            Debug.Log("And love failed that day");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartInteraction(GameObject first, GameObject second)
    {
        Debug.Log(first.name + " and " + second.name + " are trying to interact!");

        if (first == second)
        {
            // Don't interact with ourselves
            return;
        }

        InteractionComponent firstInteractionComp = first.GetComponent<InteractionComponent>();
        InteractionComponent secondInteractionComp = second.GetComponent<InteractionComponent>();
        if (firstInteractionComp && firstInteractionComp.CanInteractWith(second) &&
            secondInteractionComp && secondInteractionComp.CanInteractWith(first))
        {
            firstInteractionComp.SetInteractingPartner(second);
            secondInteractionComp.SetInteractingPartner(first);
            ++numLinks;
            UILinksText.text = "Links: " + numLinks.ToString();
        }
    }

    public void MakeCoupleFallInLove(GameObject first, GameObject second)
    {
        InteractionComponent firstInteractionComp = first.GetComponent<InteractionComponent>();
        InteractionComponent secondInteractionComp = second.GetComponent<InteractionComponent>();
        if (firstInteractionComp && !firstInteractionComp.inLove &&
            secondInteractionComp && !secondInteractionComp.inLove)
        {
            firstInteractionComp.ClearAllInteractionPartners();
            secondInteractionComp.ClearAllInteractionPartners();

            firstInteractionComp.inLove = true;
            firstInteractionComp.SetInteractingPartner(second);

            secondInteractionComp.inLove = true;
            secondInteractionComp.SetInteractingPartner(first);

            currentCouples.Add(new Couple { first = first, second = second });

            RenderHeart(first.transform.position, second.transform.position);

            UpdateCounters(first, second);
        }
    }

    public void StopInteraction(GameObject target)
    {
        InteractionComponent firstInteractionComp = target.GetComponent<InteractionComponent>();
        if (firstInteractionComp)
        {
            firstInteractionComp.ClearAllInteractionPartners();
        }
    }

    public void LethalObstacleHit(GameObject obstacle, GameObject person)
    {
        StopInteraction(person);

        obstacle.SetActive(false);
        person.SetActive(false);

        --peopleCount;

        if (person.CompareTag("Player"))
        {
            Debug.Log("Target is dead, you failed.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateCounters(GameObject first, GameObject second)
    {
        peopleCount -= 2;

        if (first.CompareTag("Player"))
        {
            --playerCount;
        }
        if (second.CompareTag("Player"))
        {
            --playerCount;
        }
    }

    void RenderHeart(Vector3 posFirst, Vector3 posSecond)
    {
        Vector3 midpoint = (posFirst + posSecond) / 2;
        Vector3 direction = (posSecond - posFirst).normalized;

        midpoint.y += 1.0f;
        Instantiate(heartPrefab, midpoint, Quaternion.identity);
    }
}
