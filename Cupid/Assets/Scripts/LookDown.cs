using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookDown : MonoBehaviour
{
    void LateUpdate()
    {
        transform.forward = -Vector3.up;
    }
}
