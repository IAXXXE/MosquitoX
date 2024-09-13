using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquito : MonoBehaviour
{
    int maxHealth = 1;
    int health = 1;
    float moveSpeed = 1f;
    float suckSpeed = 1f;

    public bool isDragging = false;
    private Vector3 offset;


    private void OnDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Debug.Log("Mouse Down");
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;

    }
    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse Up");
        isDragging = false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; 
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
   

}
