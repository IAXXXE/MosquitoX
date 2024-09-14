using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{

    static private BoxCollider2D bugActiveBox;
    
    void Awake()
    {
        bugActiveBox = transform.Find("BugActiveArea").GetComponent<BoxCollider2D>();
    }

    static public Vector2 GetAreaPos()
    {
        Bounds bounds = bugActiveBox.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        Vector2 randomPosition = new Vector2(randomX, randomY);

        return randomPosition;
    }


}

