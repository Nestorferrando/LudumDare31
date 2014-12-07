using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeriodText : MonoBehaviour {

    private static Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }

    public static void setYears(string years)
    {
        text.text = "PERIOD\n" + years;
    }
}
