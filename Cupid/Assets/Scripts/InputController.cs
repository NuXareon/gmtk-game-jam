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
    LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
        lineRenderer.numCapVertices = 8;
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

        UpdateLine();
        //DrawDebugLines();
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

                StartDrawLine(firstObject.transform.position);
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

            StopDrawLine();
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

    void StartDrawLine(Vector3 pos)
    {
        Vector3 direction = (pos - cam.transform.position).normalized;
        lineRenderer.SetPosition(0, cam.transform.position + direction);
        lineRenderer.SetPosition(1, cam.transform.position + direction);

        lineRenderer.enabled = true;
    }

    void UpdateLine()
    {
        if (firstObject)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 directionFirst = (firstObject.transform.position - cam.transform.position).normalized;
                lineRenderer.SetPosition(0, cam.transform.position + directionFirst);

                Ray cameraMouseRay = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(cameraMouseRay, out hit))
                {
                    Vector3 directionSecond = (hit.point - cam.transform.position).normalized;
                    lineRenderer.SetPosition(1, cam.transform.position + directionSecond);
                }
            }
        }
    }

    void StopDrawLine()
    {
        lineRenderer.enabled = false;
    }
}
