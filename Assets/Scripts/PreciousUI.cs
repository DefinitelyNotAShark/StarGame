using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreciousUI : MonoBehaviour
{
    private Text text;

    [HideInInspector]
    public int preciousCount;

    private int maxCount;

    private void Start()
    {
        maxCount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPreciousCount>().maxPreciousCount;
        text = GetComponent<Text>();
    }

    void Update ()
    {
        text.text = preciousCount.ToString() + "/" + maxCount;
	}
}
