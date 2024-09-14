using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        var plantTrans = PotTrans.Find(spriteName);
        plantTrans.gameObject.SetActive(true);
        plantTrans.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        plantTrans.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnComplete(() => PlayAnim(plantTrans));
    }

    private void PlayAnim(Transform transform)
    {
        transform.DOScale(new Vector3(1 + 0.07f, 1 + 0.07f, 1), 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
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
