using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    struct Couple
    {
        public GameObject first;
        public GameObject second;
    }

    public static float defaultInteractionSpeed = 0.05f;
    public static int interactionLayer = 6;

    List<Couple> currentCouples = new List<Couple>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDrawGizmos()
    {
        foreach (Couple c in currentCouples)
        {
            // TODO Render someting cute
            Vector3 midpoint = (c.first.transform.position + c.second.transform.position) / 2;
            midpoint.y += 1.0f;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(midpoint, 0.3f);
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
            Debug.DrawLine(first.transform.position, second.transform.position, Color.green, 1.0f);
        }
        else
        {
            Debug.DrawLine(first.transform.position, second.transform.position, Color.red, 1.0f);
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
        }
    }
}
