using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class IntermitentText : MonoBehaviour
    {

    private Text text;
    private Boolean enabled = true;

    private float startTime;

        void Awake()
        {
            text = GetComponent<Text>();
            startTime = Time.realtimeSinceStartup;
        }

    void Update()
    {
        text.enabled = ((((int)(Time.realtimeSinceStartup - startTime)) & 1) == 0 && enabled);


    }

    public bool Enabled
    {
        set { enabled = value; }
    }
    }

