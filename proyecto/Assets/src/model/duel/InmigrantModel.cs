using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class InmigrantModel : PersonModel
{

    private static Random random = new Random();

    protected float crouchTime;

    protected InmigrantState inmigrantState;

    public InmigrantModel(Role role) :base(role)
    {
        inmigrantState = InmigrantState.idle;
    }


    public Boolean tryToShoot()
    {
        shootTime = Time.realtimeSinceStartup;
        inmigrantState = InmigrantState.shooting;
        return true;
    }

    public new void setHiddenBehindBarrel(Boolean hidden)
    {
        if (hidden) inmigrantState = InmigrantState.crouching;
        else inmigrantState = InmigrantState.idle;
        base.setHiddenBehindBarrel(hidden);
    }


    public void startDuel()
    {
        crouchTime = Time.realtimeSinceStartup + GameRules.inmigrantMinCrouchInverval+ (float)(random.NextDouble() * GameRules.inmigrantCrouchIntervalRandom);
        _hiddenBehindBarrel = true;
        inmigrantState = InmigrantState.crouching;
    }

    public InmigrantState InmigrantState
    {
        get { return inmigrantState; }
    }


    public bool canMove()
    {
        return (base.canMove() &&  Time.realtimeSinceStartup - crouchTime > GameRules.inmigrantMinCrouchInverval);
    }
}
