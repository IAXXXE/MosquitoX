using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoSpawn : MonoBehaviour
{
    [SerializeField]public List<GameObject> mosquitoList;

    private float startTime;
    public float offset = 100f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 3f)
        {
            startTime = Time.time;
            Spawn();
        }
    }

    void Spawn()
    {

        Vector3 offPosition = GetRandomOffScreenPosition(100f, 200f);
        // Vector3 offScreenWorldPoint = Camera.main.ScreenToWorldPoint(offScreenPosition);
        var idx = UnityEngine.Random.Range(0, mosquitoList.Count);
        GameObject instance = Instantiate(mosquitoList[idx], offPosition, Quaternion.identity);

        instance.transform.parent = transform;
    }

    public Vector3 GetRandomOffScreenPosition(float offset, float minYOffset)
    {
        int direction = Random.Range(0, 3);

        float x, y;

        switch (direction)
        {
            case 0: // 上方
                x = Random.Range(0, Screen.width);
                y = Screen.height + offset;
                break;
            case 1: // 左侧
                x = -offset;
                y = Random.Range(minYOffset, Screen.height);
                break;
            case 2: // 右侧
                x = Screen.width + offset;
                y = Random.Range(minYOffset, Screen.height);
                break;
            default:
                x = 0;
                y = 0;
                break;
        }
        return Camera.main.ScreenToWorldPoint(new Vector3(x, y, Camera.main.nearClipPlane));
    }
}
