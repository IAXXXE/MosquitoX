using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoTwo : Mosquito
{
    private float startTime;

    void OnEnable()
    {
        base.OnEnable();

        moveSpeed = 2f;
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

        if (elapsedTime >= 15f)
        {
            startTime = Time.time;
            MoveToRandomPosition();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cursor"))
        {
            KillDotween();
            Vector2 contactPoint = collider.transform.position;
            Vector2 centerPoint = transform.position;

            Vector2 direction = (centerPoint - contactPoint).normalized;
            rb.AddForce(direction * 5f, ForceMode2D.Impulse);
        }
    }
}
