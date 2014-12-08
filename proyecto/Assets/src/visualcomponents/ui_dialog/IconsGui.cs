using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class IconsGui : MonoBehaviour
{

    private Boolean shootEnabled=false;
    private Boolean getInEnabled = false;

    private Boolean gameplayEnabled = false;
    private float timeGameplayEnabled;

    void Update()
    {

        SpriteRenderer inRenderer = GameObject.Find("getInIcon").GetComponent<SpriteRenderer>();
        SpriteRenderer gunRenderer = GameObject.Find("gunIcon").GetComponent<SpriteRenderer>();
        SpriteRenderer gameplayRenderer = GameObject.Find("gameplayIcon").GetComponent<SpriteRenderer>();

       gunRenderer.enabled = shootEnabled;
       inRenderer.enabled = getInEnabled;
       gameplayRenderer.enabled = gameplayEnabled;


        float alpha = (float)((Math.Sin(Time.realtimeSinceStartup*4) + 1)/4) + 0.5f;
        float alpha2 =1.5f- alpha;

        
        inRenderer.color = new Color(1, 1, 1, alpha);
        gunRenderer.color = new Color(1, 1, 1, alpha2);


        gameplayRenderer.color = new Color(1, 1, 1, Math.Min(1, Time.realtimeSinceStartup - timeGameplayEnabled));


    }

    public bool ShootEnabled
    {
        set { shootEnabled = value; }
    }

    public bool GetInEnabled
    {
        set { getInEnabled = value; }
    }

    public bool GameplayEnabled
    {
        set { gameplayEnabled = value;
            timeGameplayEnabled = Time.realtimeSinceStartup;
        }
    }
}

