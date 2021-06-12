using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public InteractionManager interactionManager;

    Camera cam;

    bool mouseDownEvent = false;
    bool mouseupEvent = false;

    GameObject firstObject;
    GameObject secondObject;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownEvent = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseupEvent = true;
        }

        if (firstObject && secondObject)
        {
            interactionManager.StartInteraction(firstObject, secondObject);

            firstObject = null;
            secondObject = null;
        }

        DrawDebugLines();
    }

    void FixedUpdate()
    {
        if (mouseDownEvent)
        {
            mouseDownEvent = false;

            firstObject = null;
            secondObject = null;

            RaycastHit hit;
            if (CameraMouseRaycast(out hit))
            {
                Debug.Log("First object selected: " + hit.collider.name);

                firstObject = hit.collider.gameObject;
            }
        }

        if (mouseupEvent)
        {
            mouseupEvent = false;

            RaycastHit hit;
            if (CameraMouseRaycast(out hit))
            {
                Debug.Log("Second object selected: " + hit.collider.name);

                secondObject = hit.collider.gameObject;
            }
            else
            {
                firstObject = null;
            }
        }
    }

    bool CameraMouseRaycast(out RaycastHit hit)
    {
        Ray cameraMouseRay = cam.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << InteractionManager.interactionLayer;
        return Physics.Raycast(cameraMouseRay, out hit, Mathf.Infinity, layerMask);
    }

    void DrawDebugLines()
    {
        if (firstObject)
        {
            if (Input.GetMouseButton(0))
            {
                Debug.DrawLine(firstObject.transform.position, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)));
            }
        }
    }
}
