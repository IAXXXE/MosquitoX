using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] List<Sprite> spriteList = new List<Sprite>();
    [SerializeField] List<int> growEnergy = new();
    
    private int maxStage = 2;
    private int currStage = 0;
    private int currEnergy = 0;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Eat()
    {
        currEnergy ++;
        Debug.Log("current energy : " + currEnergy + " grow e : " + growEnergy[currStage]) ;
        if(currEnergy >= growEnergy[currStage])
        {
            currEnergy -= growEnergy[currStage];
            Grow();
        }
    }

    void Grow()
    {
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if(currStage == 0)
        {
            spriteRenderer.gameObject.SetActive(true);
        }

        Debug.Log("curr stage : " + currStage + "max stage : " + maxStage);
        if(currStage == maxStage) return;

        spriteRenderer.sprite = spriteList[currStage];
        currStage ++;
        Debug.Log("grow " + currStage);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bug"))
        {
            Debug.Log("Eat");
            Eat();
            Destroy(collider.gameObject);
            
        }
    }

}
