using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PeriodText : MonoBehaviour {

    private Text text;
    
    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void setYears(string year)
    {
        text.text = "RAGTOWN\n" + year;
    }
}
