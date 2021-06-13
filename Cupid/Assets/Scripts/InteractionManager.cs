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

    public static float defaultInteractionSpeed = 0.05f;
    public static int interactionLayer = 6;

    public int lives = 5;

    List<Couple> currentCouples = new List<Couple>();
    int playerCount = 0;
    int peopleCount = 0;
    int interactionsOngoing = 0;

    GameUIController UIController;

    void Awake()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        playerCount = targets.Length;
        InteractionComponent[] people = GameObject.FindObjectsOfType<InteractionComponent>();
        peopleCount = people.Length;
        UIController = GameObject.FindObjectOfType<GameUIController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.InitialiseHearts(lives);
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

        if (lives <= 0)
        {
            Debug.Log("Out of lives");
        }

        InteractionComponent firstInteractionComp = first.GetComponent<InteractionComponent>();
        InteractionComponent secondInteractionComp = second.GetComponent<InteractionComponent>();
        if (firstInteractionComp && firstInteractionComp.CanInteractWith(second) &&
            secondInteractionComp && secondInteractionComp.CanInteractWith(first))
        {
            firstInteractionComp.SetInteractingPartner(second);
            secondInteractionComp.SetInteractingPartner(first);

            ++interactionsOngoing;
            --lives;
            UIController.BreakHeart();
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

            OnInteractionCompleted();
        }
    }

    public void StopInteraction(GameObject target)
    {
        InteractionComponent firstInteractionComp = target.GetComponent<InteractionComponent>();
        if (firstInteractionComp)
        {
            firstInteractionComp.ClearAllInteractionPartners();
        }

        OnInteractionCompleted();
    }

    public void LethalObstacleHit(GameObject obstacle, GameObject person)
    {
        StopInteraction(person);

        InteractionComponent interactionComp = person.GetComponent<InteractionComponent>();
        if (interactionComp)
        {
            interactionComp.isDead = true;
            IEnumerator coroutine = DeactivateGameObjectDelayed(person, 0.5f);
            StartCoroutine(coroutine);
        }

        ObstacleComponent obstacleComponent = obstacle.GetComponent<ObstacleComponent>();
        if (obstacleComponent)
        {
            obstacleComponent.isTriggered = true;
            IEnumerator coroutine = DeactivateGameObjectDelayed(obstacle, 0.333f);
            StartCoroutine(coroutine);
        }

        if (!interactionComp || !obstacleComponent)
        {
            Debug.LogWarning("Invalid call to lethal obstacle hit!");
            obstacle.SetActive(false);
            person.SetActive(false);
        }

        --peopleCount;

        if (person.CompareTag("Player"))
        {
            Debug.Log("Target is dead, you failed.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        OnInteractionCompleted();
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

    void OnInteractionCompleted()
    {
        --interactionsOngoing;
        if (interactionsOngoing == 0 && lives == 0 && playerCount > 0)
        {
            Debug.Log("Runned out of ammo.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator DeactivateGameObjectDelayed(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
