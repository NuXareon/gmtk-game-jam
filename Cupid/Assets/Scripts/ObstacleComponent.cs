using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    Animator animator;

    public AudioSource triggerAudio;

    public static int obstacleLayer = 7;

    public bool isLethal = false;

    void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public void TriggerObstacle()
    {
        if (isLethal)
        {
            animator.SetBool("Triggered", true);
            triggerAudio.Play();
        }
    }
}
