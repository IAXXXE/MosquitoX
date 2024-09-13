using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Camera mainCamera;
    private bool isCaught;

    private List<GameObject> touchBugs = new();
    private List<GameObject> catchBugs = new();

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        transform.position = targetPosition;

        if(Input.GetMouseButtonDown(0) && touchBugs.Count > 0)
        {
            foreach(var bug in touchBugs)
            {
                catchBugs.Add(bug);
                bug.GetComponent<MosquitoMid>().isCaught();
            }
        }

        if(Input.GetMouseButtonUp(0) && catchBugs.Count > 0)
        {
            catchBugs.Clear();
        }

        if(catchBugs.Count > 0)
        {
            foreach(var bug in catchBugs)
            {
                bug.transform.position = targetPosition;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isCaught == true) return;
        Debug.Log("OnTriggerEnter");
        if (collider.gameObject.CompareTag("Bug"))
        {
            Debug.Log("Touch Bug");
            touchBugs.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exit Bug");

        if(collider.gameObject == null) return;
        touchBugs.Remove(collider.gameObject);
        catchBugs.Remove(collider.gameObject);
        collider.GetComponent<Mosquito>().isDragging = false;

    }
}