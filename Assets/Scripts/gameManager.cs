using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    internal static object instance;
    public TMP_Text timeTxt;

    float alive = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        alive += Time.deltaTime;
        timeTxt.text = alive.ToString("N2");
    }
}
