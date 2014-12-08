using System;
using Assets.scripts;
using Assets.scripts.input;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public float alphaAmount = 126f;
    public float timeTransition = .001f;

    private Image img;
    private SpriteRenderer icons;

    private SpriteRenderer tutoCock;
    private SpriteRenderer tutoShoot;
    private SpriteRenderer tutoCrouch;

    private SpriteRenderer instrCock;
    private SpriteRenderer instrShoot;
    private SpriteRenderer instrCrouch;

    private bool crouchPassed, cockPassed, shootPassed = false;
    private bool init = false;

    private bool tutorialFinished = false;

    void Start()
    {
        img = GetComponentInChildren<Image>();

        icons = GetComponent<SpriteRenderer>();
        GameObject tuto;
        
        tuto = GameObject.Find("GameplayButtons_tutoCock");
        tutoCock = tuto.GetComponent<SpriteRenderer>();

        tuto = GameObject.Find("GameplayButtons_tutoShoot");
        tutoShoot = tuto.GetComponent<SpriteRenderer>();

        tuto = GameObject.Find("GameplayButtons_tutoCrouch");
        tutoCrouch = tuto.GetComponent<SpriteRenderer>();


        tuto = GameObject.Find("GameplayText_tutoCock");
        instrCock = tuto.GetComponent<SpriteRenderer>();

        tuto = GameObject.Find("GameplayText_tutoShoot");
        instrShoot = tuto.GetComponent<SpriteRenderer>();

        tuto = GameObject.Find("GameplayText_tutoCrouch");
        instrCrouch = tuto.GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (init)
        {

            if (!cockPassed)
            {
                focusCock();
            }

            if (!shootPassed && cockPassed)
            {
                focusShoot();
            }

            if (!crouchPassed && cockPassed && shootPassed)
            {
                focusCrouch();
            }
        }
    }

    public void runTutorial()
    {
        init = true;
    }

    public void focusCrouch()
    {
        //change icon animation
        pause();
        tutoCrouch.enabled = true;
        instrCrouch.enabled = true;
        
        if (InputUtils.readInput().get(InputValues.CROUCH))
        {
           
            crouchPassed = true;
            tutorialFinished = true;
            resume();
            tutoCrouch.enabled = false;
            instrCrouch.enabled = false;
        }
    }

    public void focusCock()
    {
        //change icon animation
        pause();
        tutoCock.enabled = true;
        instrCock.enabled = true;
        if (InputUtils.readInput().get(InputValues.COCK))
        {
            cockPassed = true;
            tutoCock.enabled = false;
            instrCock.enabled = false;
        }
    }

    public void focusShoot()
    {
        //change icon animation
        pause();
        tutoShoot.enabled = true;
        instrShoot.enabled = true;
        if (InputUtils.readInput().get(InputValues.SHOOT))
        {
            shootPassed = true;
            tutoShoot.enabled = false;
            instrShoot.enabled = false;
        }
    }

    public bool TutorialFinished
    {
        get { return tutorialFinished; }
    }

    public void destroy()
    {
        DestroyObject(this);
        
    }

    private void pause()
    {
        img.color = new Color(0,0,0,.65f);
        

        if (Time.timeScale == 1.0f)
        {
            Time.timeScale = 0.00001f;
        }
        

    }

    private void resume()
    {
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }
        img.color = new Color(0,0,0,0f);

    }
}
