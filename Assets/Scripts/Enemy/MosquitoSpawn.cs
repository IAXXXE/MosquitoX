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
        Spawn(new MosquitoMid());
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - startTime;

        if (elapsedTime >= 1f)
        {
            startTime = Time.time;
            Spawn(new MosquitoMid());
        }
    }

    void Spawn(Mosquito mosquito)
    {

        Vector3 offScreenPosition = GetRandomOffScreenPosition();
        Vector3 offScreenWorldPoint = Camera.main.ScreenToWorldPoint(offScreenPosition);

        GameObject instance = Instantiate(mosquitoList[0], offScreenWorldPoint, Quaternion.identity);

        instance.transform.parent = transform;
    }

    public Vector3 GetRandomOffScreenPosition()
    {
        int direction = Random.Range(0, 4);

        float x, y;

        switch (direction)
        {
            case 0:
                x = Random.Range(0, Screen.width);
                y = Screen.height + offset;
                break;
            case 1:
                x = Random.Range(0, Screen.width);
                y = -offset;
                break;
            case 2:
                x = -offset;
                y = Random.Range(0, Screen.height);
                break;
            case 3:
                x = Screen.width + offset;
                y = Random.Range(0, Screen.height);
                break;
            default:
                x = 0;
                y = 0;
                break;
        }

        return new Vector3(x, y, Camera.main.nearClipPlane);
    }
}
