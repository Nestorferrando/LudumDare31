using System;
using System.Collections.Generic;
using UnityEngine;


public class CityModel
{

    private Dictionary<Role,int> population;

    public CityModel()
    {
        population=new Dictionary<Role, int>();
        
        population.Add(Role.BusinessMan,50);
        population.Add(Role.Criminal, 50);
        population.Add(Role.Indian, 50);
        population.Add(Role.Miner, 50); 
    }


    public int getTotalPopulation()
    {
        return getPopulation(Role.BusinessMan) + getPopulation(Role.Criminal) + getPopulation(Role.Indian) + getPopulation(Role.Miner);
    }

    public int getBalancedPopulation()
    {
        int BusinessManPopulation = getPopulation(Role.BusinessMan);
        int CriminalPopulation = getPopulation(Role.Criminal);
        int IndianPopulation = getPopulation(Role.Indian);
        int MinerPopulation = getPopulation(Role.Miner);

        return Math.Min(BusinessManPopulation, Math.Min(CriminalPopulation, Math.Min(IndianPopulation, MinerPopulation)));
    }

    private int getPopulation(Role role)
    {
        int temp = 0;
        population.TryGetValue(role, out temp);
        return temp;
    }


    public void AddIndividual(Role role)
    {
        population[role] = population[role]+1;
    }

    public void removePopulationFromAllRoles(int amount)
    {
        population[Role.BusinessMan] = Math.Max(population[Role.BusinessMan] - amount,0);

    }



    public CityStatus getCityUnbalance()
    {
        int balancedPopulation = getBalancedPopulation();
      
        float neutralBalance = 1.0f;
        float indianUnbalance = (getPopulation(Role.Indian) - balancedPopulation)/(float)balancedPopulation;
        float businessUnbalance = (getPopulation(Role.BusinessMan) - balancedPopulation) / (float)balancedPopulation;
        float criminalUnbalance = (getPopulation(Role.Criminal) - balancedPopulation) / (float)balancedPopulation;
        float minerUnbalance = (getPopulation(Role.Miner) - balancedPopulation) / (float)balancedPopulation;
      
        return new CityStatus(getTotalPopulation(), neutralBalance, indianUnbalance, businessUnbalance, criminalUnbalance, minerUnbalance);
    }


}
