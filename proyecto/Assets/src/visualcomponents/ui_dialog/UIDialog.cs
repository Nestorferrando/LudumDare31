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
    private bool sheriffSurrender;
    public  bool sheriffDied = false;

    private bool stopped = false;

    public UIDialog init(float delay, Font font, int fontSize, float fadeSpeed, bool sheriff, bool sheriffSurrender)
    {
        this.delay = delay;
        this.font = font;
        this.fontSize = fontSize;
        this.fadeSpeed = fadeSpeed;
        this.sheriff = sheriff;
        this.sheriffSurrender = sheriffSurrender;

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

        if (sheriffSurrender)
        {
            dialog.text = Dialog.getSheriffSurrenderDialog();

        }

	    if (!sheriffDied)
	    {
	        if (!sheriff)
	        {
	            dialog.text = Dialog.getWaveDialog(role);
	        }
	        else
	        {
	            if (sheriffSurrender)
	            {
	                dialog.text = Dialog.getSheriffSurrenderDialog();
                    
	            }
	            else
	            {
	                dialog.text = Dialog.getSheriffDialog();
	            }
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
	
	void Update () {
	    if (Time.fixedTime > startTime + delay || duel || stopped)
	    {
	        fadeToAlpha();
            DestroyObject(GameObject.Find("SheriffText").GetComponent<Text>());
            DestroyObject(GameObject.Find("WaveText").GetComponent<Text>());
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

    internal void stop()
    {
        this.stopped = true;
    }
}
