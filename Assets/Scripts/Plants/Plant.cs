using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Transform PotTrans;

    [SerializeField] List<int> growEnergy = new();
    
    private int maxStage;
    private int currStage = 0;
    private int currEnergy = 0;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        PotTrans = transform.Find("PotSprite");
        maxStage = PotTrans.childCount;
    }

    void Eat()
    {
        currEnergy ++;
        if(currEnergy >= growEnergy[currStage])
        {
            currEnergy -= growEnergy[currStage];
            Grow();
        }
    }

    void Grow()
    {
        if(currStage >= maxStage)
        {
            Skill();
            return;
        }

        foreach(Transform sprite in PotTrans)
        {
            sprite.gameObject.SetActive(false);   
        }
        currStage ++;
        var spriteName = "Sprite_" + currStage.ToString();
        // spriteRenderer = transform.Find(spriteName).GetComponent<SpriteRenderer>();
        PotTrans.Find(spriteName).gameObject.SetActive(true);

    }

    protected virtual void Skill()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bug"))
        {
            Eat();
            Destroy(collider.gameObject);
        }
    }

}
