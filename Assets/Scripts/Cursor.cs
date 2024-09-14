using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Camera mainCamera;

    private Vector2 offset = new Vector2(0.2f, 0.2f);

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
        var pos = new Vector3(targetPosition.x + offset.x, targetPosition.y + offset.y, targetPosition.z);
        transform.position = pos;

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

    public bool LetBugsGo()
    {
        bool hasBug = false;
        if (catchBugs.Count > 0) hasBug = true;
        touchBugs.Clear();
        catchBugs.Clear();
        return hasBug;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isCaught == true) return;
        if (collider.gameObject.CompareTag("Bug"))
        {
            touchBugs.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject == null) return;
        touchBugs.Remove(collider.gameObject);
        catchBugs.Remove(collider.gameObject);
        if(collider.GetComponent<Mosquito>() == null) return;
        collider.GetComponent<Mosquito>().isDragging = false;

    }
}