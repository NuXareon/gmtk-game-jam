using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    InteractionManager interactionManager;

    Rigidbody rigidBody;

    GameObject interactionPartner;
    public bool inLove
    {
        get; set;
    }

    //float interactionSpeed = InteractionManager.defaultInteractionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        interactionManager = GameObject.FindWithTag("GameController").GetComponent<InteractionManager>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        inLove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inLove)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (inLove)
        {
            // Interaction already finished (turn them into obstacles?)
            return;
        }

        if (collision.collider.gameObject.layer == InteractionManager.interactionLayer)
        {
            interactionManager.MakeCoupleFallInLove(gameObject, collision.collider.gameObject);
        }
        else if (false)
        {
            // Handle obstacle collisions
        }

        // finish interaction for the other player?
        // interactionPartner = null;
    }

    void FixedUpdate()
    {
        if (interactionPartner && !inLove)
        {
            Vector3 direction = interactionPartner.transform.position - gameObject.transform.position;
            rigidBody.MovePosition(gameObject.transform.position + direction.normalized * InteractionManager.defaultInteractionSpeed);
        }
    }

    public bool CanInteractWith(GameObject otherObj)
    {
        if (interactionPartner)
        {
            // Already interacting (can we switch targets?)
            return false;
        }

        if (otherObj.layer != InteractionManager.interactionLayer)
        {
            // Object not in valid layer (i.e. not a person)
            return false;
        }

        return true;
    }

    public void SetInteractingPartner(GameObject partnerObj)
    {
        interactionPartner = partnerObj;
    }

    public void ClearAllInteractionPartners()
    {
        if (interactionPartner)
        {
            InteractionComponent partnerInteraction = interactionPartner.GetComponent<InteractionComponent>();
            if (partnerInteraction)
            {
                partnerInteraction.interactionPartner = null;
            }
        }

        interactionPartner = null;
    }
}
