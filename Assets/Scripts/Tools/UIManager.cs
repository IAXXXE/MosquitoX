using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void ShowTips()
    {
        transform.Find("Tips").gameObject.SetActive(true);
    }

    public void HideTips()
    {
        transform.Find("Tips").gameObject.SetActive(false);
    }
}
