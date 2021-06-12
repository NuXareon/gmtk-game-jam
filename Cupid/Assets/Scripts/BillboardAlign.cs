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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        gameObject.transform.forward = cam.transform.forward;
    }
}
