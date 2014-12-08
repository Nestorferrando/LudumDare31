using System;
using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackCurtain : MonoBehaviour
{
    public float fadeSpeed = 1f;
    public float delayBetweenFades = 3f;
    private Image img;
    private PeriodText textPeriod;
    private StatisticsText textStatistics;
    private GameObject child;
    private ManageUIDialogs manageDialogs;

    private float fadeStart;
	// Use this for initialization

	void Start () {
        img = GetComponent<Image>();
        textPeriod = GameObject.Find("Period").GetComponent<PeriodText>();
        textStatistics = GameObject.Find("Statistics").GetComponent<StatisticsText>();

	    GameObject parent = transform.parent.gameObject;
	    

	    GameObject canvas = GameObject.Find("Canvas");
	    manageDialogs = canvas.GetComponent<ManageUIDialogs>();
	    

	}
	
	// Update is called once per frame
	void Update ()
	{

	}


    public bool fadeFinished()
    {
        return Time.realtimeSinceStartup - fadeStart > delayBetweenFades;
    }
    public void fadeToBlackPeriod(String text)
    {
        textPeriod.setYears(text);
        showPeriod();
        this.fadeToBlack();
    }

    public void fadeToAlphaPeriod()
    {
        hidePeriod();
        this.fadeToAlpha();
    }


    public void fadeToBlackStatistics(String text)
    {
        textPeriod.setYears(text);
        showStatistics();
        this.fadeToBlack();
    }

    public void fadeToAlphaStatistics()
    {
        hideStatistics();
        this.fadeToAlpha();
    }


    private void fadeToBlack()
    {
        fadeStart = Time.realtimeSinceStartup;
        img.CrossFadeAlpha(255f, fadeSpeed, false); // sec  
        manageDialogs.stop();
    }

    private void fadeToAlpha()
    {
        fadeStart = Time.realtimeSinceStartup;
        img.CrossFadeAlpha(0f, fadeSpeed, false); // sec
        //text.CrossFadeAlpha(0f, fadeSpeed, false);

    }

    private void showPeriod()
    {
        child = GameObject.Find("Period");
        Text childText = child.GetComponent<Text>();
        childText.enabled = true;
    }

    private  void hidePeriod()
    {
        child = GameObject.Find("Period");
        Text childText = child.GetComponent<Text>();
        childText.enabled = false;
    }

    private void showStatistics()
    {
        child = GameObject.Find("Statistics");
        Text childText = child.GetComponent<Text>();
        childText.enabled = true;
        Debug.Log("hi");
    }

    private void hideStatistics()
    {
        child = GameObject.Find("Statistics");
        Text childText = child.GetComponent<Text>();
        childText.enabled = false;
    }

}
