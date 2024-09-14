using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MosquitoMid : Mosquito
{
    private float startTime;

    void OnEnable()
    {
        base.OnEnable();

        MoveToRandomPosition();
        startTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        if(isDragging == true) 
        {
            return;
        }

        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 10f)
        {
            startTime = Time.time;
            MoveToRandomPosition();
        }

    }


    
    // void OnMouseDown()
    // {
    //     Debug.Log("Mouse Down");
    //     offset = transform.position - GetMouseWorldPosition();
    //     isDragging = true;

    // }
    // void OnMouseDrag()
    // {
    //     if (isDragging)
    //     {
    //         transform.position = GetMouseWorldPosition() + offset;
    //     }
    // }

    // void OnMouseUp()
    // {
    //     Debug.Log("Mouse Up");
    //     isDragging = false;
    // }

    // private Vector3 GetMouseWorldPosition()
    // {
    //     Vector3 mousePosition = Input.mousePosition;
    //     mousePosition.z = 10; 
    //     return Camera.main.ScreenToWorldPoint(mousePosition);
    // }

    // public void OnPointerEnter(PointerEventData eventData)
    // {
    //     Debug.Log("Mouse Enter");
    // }
}
