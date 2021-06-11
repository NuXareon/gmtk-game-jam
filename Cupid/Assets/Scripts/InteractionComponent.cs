using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    Rigidbody rigidBody;

    GameObject interactionPartner;

    //float interactionSpeed = InteractionManager.defaultInteractionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (interactionPartner)
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

        if (otherObj.layer != 6)
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
}
