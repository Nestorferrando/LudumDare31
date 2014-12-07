using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.src.visualcomponents;
using UnityEngine;


public  class DuelStreet : MonoBehaviour
{


    private readonly Vector2 inmigrant1InitialPosition = new Vector2(11.04f, -2.05f);
    private readonly Vector2 inmigrant2InitialPosition = new Vector2(11.04f, -2.05f);
    private readonly Vector2 inmigrant3InitialPosition = new Vector2(11.53f, -4.39f);


    private bool duelEnabled;
    private DuelResult duelResult;

    private Person[] inmigrants;
    private int activeInmigrants;

    private Sheriff sheriff;



    // Use this for initialization
    private void Start()
    {
        sheriff =  GetComponentsInChildren<Sheriff>()[0];
        inmigrants = GetComponentsInChildren<Person>();
        duelEnabled = false;

    }

    public void resetWave(List<Role> roles)
    {

        for (int i = 0; i < roles.Count; i++)
        {
            inmigrants[i].Model=new PersonModel(roles.ElementAt(i));
        }

        inmigrants[0].moveInstantlyToPosition(inmigrant1InitialPosition);
        inmigrants[1].moveInstantlyToPosition(inmigrant2InitialPosition);
        inmigrants[2].moveInstantlyToPosition(inmigrant3InitialPosition);

        activeInmigrants = roles.Count;

    }

    public void updateSheriffModel(SheriffModel model)
    {
        sheriff.Model = model;
    }


    public void moveWave()
    {
        for (int i = 0; i < activeInmigrants; i++)
        {
            inmigrants[i].startMoving();
        }


    }

    public void stopWave()
    {
        for (int i = 0; i < activeInmigrants; i++)
        {
            inmigrants[i].stopMoving();
        }
    }

    public void startDuelMode()
    {
        duelEnabled = true;
    }


    public bool isDuelModeActive()
    {
        return duelEnabled;
    }

    public int getSheriffBullets()
    {
        return sheriff.Model.Bullets;
    }

    public DuelResult getDuelResult()
    {
        return duelResult;
    }


    private void FixedUpdate()
    {

    }



}

