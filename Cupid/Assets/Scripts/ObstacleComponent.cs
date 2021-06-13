using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    Animator animator;

    public static int obstacleLayer = 7;

    public bool isLethal = false;

    public bool isTriggered { get; set; }

    void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isTriggered)
        {
            animator.SetBool("Triggered", true);
        }
    }
}
