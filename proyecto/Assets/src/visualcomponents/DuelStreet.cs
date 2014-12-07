using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Assets.scripts;
using Assets.scripts.input;
using Assets.src.visualcomponents;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;


public class DuelStreet : MonoBehaviour
{


    private static Random random = new Random();

    private readonly Vector2 inmigrant1InitialPosition = new Vector3(11.04f, -2.05f,0);
    private readonly Vector2 inmigrant2InitialPosition = new Vector3(12.43f, -3.06f,-0.1f);
    private readonly Vector2 inmigrant3InitialPosition = new Vector3(11.53f, -4.39f,-0.2f);


    private bool duelEnabled;
    private DuelResult duelResult;

    private Inmigrant[] inmigrants;
    private int activeInmigrants;

    private Sheriff sheriff;

    private FrontBarrel frontBarrel;



    // Use this for initialization
    private void Start()
    {
        sheriff = GetComponentsInChildren<Sheriff>()[0];
        inmigrants = GetComponentsInChildren<Inmigrant>();
        frontBarrel = GetComponentsInChildren<FrontBarrel>()[0];
        duelEnabled = false;

    }

    public void resetPeriod(SheriffModel model)
    {
        sheriff.Model = model;
    }

    public void resetWave(Role[] roles)
    {

        for (int i = 0; i < roles.Length; i++)
        {
            inmigrants[i].Model = new InmigrantModel(roles[i]);
        }

        inmigrants[0].moveInstantlyToPosition(inmigrant1InitialPosition);
        inmigrants[1].moveInstantlyToPosition(inmigrant2InitialPosition);
        inmigrants[2].moveInstantlyToPosition(inmigrant3InitialPosition);

        activeInmigrants = roles.Length;

    }

    public void updateSheriffModel(SheriffModel model)
    {
        sheriff.Model = model;
    }

    public SheriffModel getSheriffModel()
    {
        return sheriff.Model;
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

        for (int i = 0; i < activeInmigrants; i++)
        {
            inmigrants[i].crouch();
            inmigrants[i].Model.startDuel();
        }
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

        if (!duelEnabled) return;

        InputResult result = InputUtils.readInput();


        if (sheriff.Model.Bullets == 0)
        {
            duelResult = DuelResult.surrender;
            duelEnabled = false;
            return;
        }



        //sheriff operations

        bool sheriffShoots = performSheriffActions(result);

        if (sheriffShoots)
            {
                tryToKillEnemy();  
            }

        //enemy operations
        if (getAliveEnemies() == 0)
        {
            duelResult = DuelResult.won;
            duelEnabled = false;
            return;
        }

        for (int i = 0; i < activeInmigrants; i++)
        {
            if (inmigrants[i].Model.Alive && inmigrants[i].Model.canMove())
            {
                var enemyShoots = performEnemyActions(i);
                if (enemyShoots && !sheriff.Model.HiddenBehindBarrel && random.NextDouble()<GameRules.enemiesKillSuccessRatio)
                {
                    sheriff.Model.Alive = false;
                    sheriff.performDieAnimation();
                }
            }
        }


        if (!sheriff.Model.Alive)
        {
            duelResult = DuelResult.dead;
            duelEnabled = false;
            return;  
        }
    }

    private bool performEnemyActions(int i)
    {
        bool enemyShoots = false;

        switch (inmigrants[i].Model.State)
        {
            case PersonState.crouch:
                inmigrants[i].standUp();
                break;

            case PersonState.idle:
                inmigrants[i].Model.tryToShoot();
                inmigrants[i].performShootAnimation();
                enemyShoots = true;
                break;
            case PersonState.shooting:
                inmigrants[i].crouch();
                break;
        }
        return enemyShoots;
    }

    private int getAliveEnemies()
    {
        int aliveEnemies = 0;
        for (int i = 0; i < activeInmigrants; i++)
        {
            if (inmigrants[i].Model.Alive)
            {
                aliveEnemies++;
            }
        }
        return aliveEnemies;
    }

    private void tryToKillEnemy()
    {
        if (random.NextDouble() < GameRules.myKillSuccessRatio)
        {
            for (int i = 0; i < activeInmigrants; i++)
            {
                if (inmigrants[i].Model.Alive && !inmigrants[i].Model.HiddenBehindBarrel)
                {
                    inmigrants[i].Model.Alive = false;
                    inmigrants[i].performDieAnimation();
                    return;
                }
            }
        }
    }




    private bool performSheriffActions(InputResult result)
    {
        bool sheriffShoots = false;

        if (sheriff.Model.canMove())
        {


            if (result.get(InputValues.CROUCH))
            {
                sheriff.crouch();
            }
            else
            {

                sheriff.standUp();
            }

            if (result.get(InputValues.COCK) && sheriff.Model.cock())
            {
                sheriff.performCockAnimation();
            }

            if (result.get(InputValues.SHOOT) && sheriff.Model.tryToShoot())
            {
                sheriff.performShootAnimation();
                sheriffShoots = true;
            }
        }
        return sheriffShoots;
    }

    public bool enemiesCloseFromFrontBarrels()
    {
        if (inmigrants[2].transform.position.x < frontBarrel.transform.position.x)
        {
            return true;
        }
        return false;
    }

}

