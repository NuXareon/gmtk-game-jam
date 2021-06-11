using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static float defaultInteractionSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartInteraction(GameObject first, GameObject second)
    {
        Debug.Log(first.name + " and " + second.name + " are trying to interact!");

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
}
