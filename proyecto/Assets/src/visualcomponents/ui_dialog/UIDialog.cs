using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    public Font font;
    public int fontSize = 12;
    [Range(0, 1f)] public float fadeSpeed = .15f;
    private Text dialog;
    private float delay;
    private float startTime;
    private Image img;
    private GameObject child;



	void Start ()
	{
	    child = GameObject.Find("Text");
	    dialog = child.AddComponent<Text>();
	    dialog.font = font;
	    dialog.fontSize = fontSize;
        delay = 1; //s

	    dialog.text = Dialog.getDialog(Role.BusinessMan);
	    startTime = Time.fixedTime;


	    img = this.GetComponent<Image>();

	    fadeOut();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Time.fixedTime > startTime + delay)
	    {
	        fadeIn();
            DestroyObject(child.GetComponent<Text>());
            DestroyObject(this);
	    }

	}

    void setDimension()
    {
        
    }

    void fadeIn()
    {
        img.CrossFadeAlpha(0f, fadeSpeed, false); // sec
    }

    void fadeOut()
    {
        img.CrossFadeAlpha(100f, fadeSpeed, false); // sec
    }
}
