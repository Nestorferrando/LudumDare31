﻿using System;
using Debug = UnityEngine.Debug;


public class InmigrationUtils
    {

    private static Random random = new Random();



    
    public static Role getRandomRole()
    {
        double rnd = random.NextDouble();

        if (rnd<0.25) return Role.BusinessMan;
        if (rnd < 0.5) return Role.Miner;
        if (rnd < 0.75) return Role.Indian;
        return Role.Criminal;


    }

    public static Role[] getInmigrants(SheriffModel sheriff, CityStatus cityStatus)
        {

            Role[] returnRoles = new Role[GameRules.inmigrantWaveSize];
            Role predominantRole = sheriff.AntiRole;


            if (random.NextDouble() < GameRules.antiRoleSpawnProbability)
            {
                for (int i = 0; i < returnRoles.Length; i++)
                {
                    returnRoles[i] = predominantRole;
                }
            }
            else
            {
                Role[] otherRoles = getOtherRoles(predominantRole);
                Role chosenRole = otherRoles[(int)(random.NextDouble() * 3)];

               

                for (int i = 0; i < returnRoles.Length; i++)
                {
                    returnRoles[i] = chosenRole;
                }
            }
            return returnRoles;
        }

        private static Role[] getOtherRoles(Role predominantRole)
        {
            Role[] otherRoles = new Role[3];

            int otherRolesCounter = 0;

            if (Role.BusinessMan != predominantRole)
            {
                otherRoles[otherRolesCounter] = Role.BusinessMan;
                otherRolesCounter++;
            }

            if (Role.Indian != predominantRole)
            {
                otherRoles[otherRolesCounter] = Role.Indian;
                otherRolesCounter++;
            }

            if (Role.Miner != predominantRole)
            {
                otherRoles[otherRolesCounter] = Role.Miner;
                otherRolesCounter++;
            }

            if (Role.Criminal != predominantRole)
            {
                otherRoles[otherRolesCounter] = Role.Criminal;
                otherRolesCounter++;
            }
            return otherRoles;
        }
    }
