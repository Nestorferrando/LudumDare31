using System;
using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackCurtain : MonoBehaviour
{
    public float fadeSpeed = 1f;
    private Image img;
    private Text textPeriod;
    private Text textStatistics;
    private GameObject child;

    private float timeIni;
	// Use this for initialization

	void Start () {
        img = GetComponent<Image>();
	    textPeriod = GameObject.Find("Statistics").GetComponent<Text>();
	    textStatistics = GameObject.Find("Period").GetComponent<Text>();
        
        /*
	    StatisticsText.setStatistics("Your city has burnt", 10, 4, 9, 1, 100, "SUICIDAL", "4/5");

        fadeToBlack(textStatistics);

	    showStatistics();
        

	    PeriodText.setYears("1980-1990");
        */
	}
	
	// Update is called once per frame
	void Update ()
	{
        /*
	    if (Time.fixedTime > timeIni + 2)
	    {
	        fadeToAlpha(textStatistics);
	        hideStatistics();
	    }
         * */
	}

    void fadeToBlack(Text text)
    {
        img.CrossFadeAlpha(255f, fadeSpeed, false); // sec        
        //text.CrossFadeAlpha(255f, fadeSpeed, false);
    }

    void fadeToAlpha(Text text)
    {
        img.CrossFadeAlpha(0f, fadeSpeed, false); // sec
        //text.CrossFadeAlpha(0f, fadeSpeed, false);

    }

    void showPeriod()
    {
        child = GameObject.Find("Period");
        Text childText = child.GetComponent<Text>();
        childText.enabled = true;
        Debug.Log("hi");
    }

    void hidePeriod()
    {
        child = GameObject.Find("Period");
        Text childText = child.GetComponent<Text>();
        childText.enabled = false;
    }

    void showStatistics()
    {
        child = GameObject.Find("Statistics");
        Text childText = child.GetComponent<Text>();
        childText.enabled = true;
        Debug.Log("hi");
    }

    void hideStatistics()
    {
        child = GameObject.Find("Statistics");
        Text childText = child.GetComponent<Text>();
        childText.enabled = false;
    }

}
