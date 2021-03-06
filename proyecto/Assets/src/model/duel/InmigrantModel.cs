﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class InmigrantModel : PersonModel
{

    

    protected float crouchTime;

    protected PersonState state;

    public InmigrantModel(Role role) :base(role)
    {
        state = PersonState.idle;
    }


    public Boolean tryToShoot()
    {
        shootTime = Time.realtimeSinceStartup;
        state = PersonState.shooting;
        return true;
    }

    public new void setHiddenBehindBarrel(Boolean hidden)
    {
        if (hidden)
        {
            state = PersonState.crouch;
            crouchTime = Time.realtimeSinceStartup + (float)(random.NextDouble() * GameRules.inmigrantCrouchIntervalRandom);
        }
        else
        {
            state = PersonState.idle;
            standUpTime = Time.realtimeSinceStartup;
        }
        base.setHiddenBehindBarrel(hidden);
    }


    public void startDuel(bool firstTime)
    {
        crouchTime = Time.realtimeSinceStartup +  (float)(random.NextDouble() * GameRules.inmigrantCrouchIntervalRandom);
        //three additional seconds if it is the first time
        if (firstTime) crouchTime += 3;
        _hiddenBehindBarrel = true;
        state = PersonState.crouch;
    }

    public PersonState State
    {
        get { return state; }
    }


    public new bool canMove()
    {
        return (  (Time.realtimeSinceStartup - shootTime > GameRules.InmigrantsShootSpeed) && 
            
            base.canMove() && Time.realtimeSinceStartup - standUpTime > GameRules.inmigrantDelayBetWeenStandAndShoot && Time.realtimeSinceStartup - crouchTime > GameRules.inmigrantMinCrouchInverval);
    }
}
