using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    InteractionManager interactionManager;

    Rigidbody rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Component halo;

    GameObject interactionPartner;
    public bool inLove
    {
        get; set;
    }

    //float interactionSpeed = InteractionManager.defaultInteractionSpeed;

    void Awake()
    {
        interactionManager = GameObject.FindWithTag("GameController").GetComponent<InteractionManager>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        halo = GetComponent("Halo");
        inLove = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // This doesn't seem to work
        spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
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
        else
        {
            ObstacleComponent obstacleComponent = collision.collider.gameObject.GetComponent<ObstacleComponent>();
            if (obstacleComponent)
            {
                if (obstacleComponent.isLethal)
                {
                    interactionManager.LethalObstacleHit(collision.collider.gameObject, gameObject);
                }
                else
                {
                    // Correct position so we don't finish inside the object, a bit hacky
                    ContactPoint contact = collision.GetContact(0);
                    Vector3 direction = interactionPartner.transform.position - gameObject.transform.position;
                    Vector3 correctedPos = gameObject.transform.position - direction.normalized * 0.1f;
                    rigidBody.MovePosition(correctedPos);

                    interactionManager.StopInteraction(gameObject);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (!inLove)
        {
            halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
        }
    }

    void OnMouseExit()
    {
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }

    void FixedUpdate()
    {
        if (interactionPartner && !inLove)
        {
            Vector3 direction = interactionPartner.transform.position - gameObject.transform.position;
            rigidBody.MovePosition(gameObject.transform.position + direction.normalized * InteractionManager.defaultInteractionSpeed);
        }
    }
    
    void UpdateAnimation()
    {
        if (inLove)
        {
            animator.SetBool("InLove", true);
            return;
        }

        if (interactionPartner)
        {
            animator.SetBool("Idle", false);

            Vector3 direction = (interactionPartner.transform.position - gameObject.transform.position).normalized;
            if (Vector3.Dot(direction, Vector3.forward) > 0.707f)
            {
                animator.SetInteger("Direction", 0);
            }
            else if (Vector3.Dot(direction, Vector3.right) > 0.707f)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (Vector3.Dot(direction, -Vector3.forward) > 0.707f)
            {
                animator.SetInteger("Direction", 2);
            }
            else
            {
                animator.SetInteger("Direction", 3);
            }

        }
        else
        {
            animator.SetBool("Idle", true);
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

        Vector3 direction = otherObj.transform.position - gameObject.transform.position;
        float maxDistance = 1.5f; // TODO adjust this
        int layerMask = 1 << ObstacleComponent.obstacleLayer;
        if (Physics.Raycast(gameObject.transform.position, direction.normalized, maxDistance, layerMask))
        {
            // If we are too close to an obstacle don't move
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
            if (partnerInteraction && !partnerInteraction.inLove)
            {
                partnerInteraction.interactionPartner = null;
            }
        }

        if (!inLove)
        {
            interactionPartner = null;
        }
    }
}
