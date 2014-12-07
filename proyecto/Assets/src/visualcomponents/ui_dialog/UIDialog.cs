using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    public bool duel = false;
    
    [Range(0f,4f)]
    private float delay; // seconds
    private Font font;
    private int fontSize;
    [Range(0, 1f)] 
    private float fadeSpeed;

    private Text dialog;
    private float startTime;
    private Image img;
    private GameObject child;
    private Role role;

    private bool sheriff;
    public  bool sheriffDied = false;
    

    public UIDialog init(float delay, Font font, int fontSize, float fadeSpeed, bool sheriff)
    {
        this.delay = delay;
        this.font = font;
        this.fontSize = fontSize;
        this.fadeSpeed = fadeSpeed;
        this.sheriff = sheriff;

        return this;
    }

    public UIDialog setRole(Role role)
    {
        this.role = role;

        return this;
    }

	void Start ()
	{

	    if (sheriff)
	        child = GameObject.Find("SheriffText");
	    else
	        child = GameObject.Find("WaveText");

	    dialog = child.AddComponent<Text>();
	    dialog.font = font;
	    dialog.fontSize = fontSize;
	    dialog.alignment = TextAnchor.MiddleCenter;

	    if (!sheriffDied)
	    {
	        if (!sheriff)
	        {
	            dialog.text = Dialog.getWaveDialog(role);
	        }
	        else
	        {
	            dialog.text = Dialog.getSheriffDialog();
	        }
	    }
	    else
	    {
	        dialog.text = Dialog.getToggleSheriffDialog();
	    }

	    startTime = Time.fixedTime;

	    img = this.GetComponent<Image>();

	    fadeFromAlpha();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Time.fixedTime > startTime + delay || duel)
	    {
	        fadeToAlpha();
            DestroyObject(child.GetComponent<Text>());
            DestroyObject(this);
	    }

	}

    public void fadeToAlpha()
    {
        img.CrossFadeAlpha(0f, fadeSpeed, false); // sec
    }

    public void fadeFromAlpha()
    {
        img.CrossFadeAlpha(100f, fadeSpeed, false); // sec
    }


    internal void startDuel()
    {
        duel = true;
    }

    internal void setSheriffDied(bool died)
    {
        sheriffDied = died;
    }
}
