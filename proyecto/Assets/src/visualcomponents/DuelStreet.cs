using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public  class DuelStreet : MonoBehaviour
{



    private bool duelEnabled;
    private int activeCharacters;

    private Sheriff sheriff;
    private Person[] inmigrants;




    // Use this for initialization
    private void Start()
    {
        sheriff =  GetComponentsInChildren<Sheriff>()[0];
        inmigrants = GetComponentsInChildren<Person>();
    }

    public void resetWave(List<Role> roles)
    {
        
    }


    public void moveWave()
    {

    }

    public void stopWave()
    {

    }

    public void startDuelMode()
    {

    }

    public void stopDuelMode()
    {

    }

    private void FixedUpdate()
    {

    }



}

