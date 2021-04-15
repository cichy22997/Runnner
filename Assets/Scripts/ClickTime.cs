using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTime : MonoBehaviour
{

    public static ClickTime Instance { get; set; }
    public float lastClickTime;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        lastClickTime += Time.deltaTime;
    }

    public void OnClick()
    {
        lastClickTime = 0;
    }
}
