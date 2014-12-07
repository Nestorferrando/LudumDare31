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

        public InmigrantModel(Role role) :base(role)
    {

    }


    public Boolean tryToShoot()
    {
        shootTime = Time.realtimeSinceStartup);
        return true;
    }

    public void startDuel()
    {
        crouchTime = Time.realtimeSinceStartup + GameRules.inmigrantMinCrouchInverval+ (float)(random.NextDouble() * GameRules.inmigrantCrouchIntervalRandom);
        _hiddenBehindBarrel = true;
    }


    public bool canMove()
    {
        return (base.canMove() &&  Time.realtimeSinceStartup - crouchTime > GameRules.inmigrantMinCrouchInverval);
    }
}
