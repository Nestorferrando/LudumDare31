﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


   public class GameRules
    {

        //---------------------------------------
       public static readonly float totalInfestation=0.25f;
       //---------------------------------------
       public static readonly int totalPeriods = 8;
       public static readonly int startingYear = 1840;
       public static readonly int yearsPerPeriod = 1;
       //---------------------------------------
       public static readonly float antiRoleSpawnProbability = 0.5f;
       public static readonly int inmigrantWaveSize = 3;
        //---------------------------------------
       public static readonly int wavesPerPeriod = 3;

       public static readonly int sheriffBullets = 9;

        //---------------------------------------
       public static readonly float myKillSuccessRatio = 1.0f;
       public static readonly float enemiesKillSuccessRatio = 0.9f;

       //---------------------------------------

       public static readonly float SheriffShootSpeed = 0.025f;
       public static readonly float InmigrantsShootSpeed = 0.2f;

       public static readonly float sheriffCockSpeed = 0.01f;



       public static readonly float inmigrantMinCrouchInverval = 0.75f;
       public static readonly float inmigrantCrouchIntervalRandom = 2f;


       public static readonly float inmigrantDelayBetWeenStandAndShoot = 0.5f;



    }

