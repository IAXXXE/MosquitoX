using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MosquitoMid : Mosquito
{
    private float startTime;

    void OnEnable()
    {
        MoveToRandomPosition();
        startTime = Time.time;
    }

    public void isCaught()
    {
        Debug.Log("isDrag");
        isDragging = true;
        specificTween.Kill();
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

    private Tween specificTween;
    void MoveToRandomPosition()
    {
        Vector3 randomScreenPoint = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.nearClipPlane);

        Vector3 randomWorldPoint = Camera.main.ScreenToWorldPoint(randomScreenPoint);

        specificTween = transform.DOMove(randomWorldPoint, 5f);
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse Enter");
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
