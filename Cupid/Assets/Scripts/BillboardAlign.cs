using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardAlign : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    void LateUpdate()
    {
        gameObject.transform.forward = cam.transform.forward;
    }
}
