using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MosquitoThree : Mosquito
{
    private float startTime;

    private string statt;

    void OnEnable()
    {
        base.OnEnable();

        moveSpeed = 6f;
        MoveToRandomPosition();
        startTime = Time.time;
    }

    void Update()
    {
        if(isDragging == true) 
        {
            return;
        }

        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 2f)
        {
            if(statt == "tired") return;
            startTime = Time.time;
            MoveToCursor();
        }

        if(elapsedTime >= 5f)
        {
            startTime = Time.time;
            statt = "normal";
        }
    }

    public override void isCaught()
    {
        if(!(statt == "tired")) return;
        isDragging = true;
        specificTween.Kill();
    }

    private void MoveToCursor()
    {
        var mainCamera = Camera.main;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        if(targetPosition.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        // Vector2 pos2d = AreaManager.GetAreaPos();
        // Vector3 randomWorldPoint = new Vector3(pos2d.x, pos2d.y, 0);

        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(targetPosition.x, targetPosition.y));
        specificTween = transform.DOMove(targetPosition, distance/moveSpeed);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(statt == "tired") return;

        if (collider.gameObject.CompareTag("Cursor"))
        {
            KillDotween();
            var hasBug = collider.transform.GetComponent<Cursor>().LetBugsGo();
            if(hasBug) statt = "tired";
        }
    }

}
