using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsText : MonoBehaviour {

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }


    public void setStatistics(int survivedPeriods, int timesSheriffChanged, CityStatus cityStatus)
    {
        string message="";
        if (survivedPeriods == GameRules.totalPeriods)
        {
            message = "CONGRATULATONS!, your city has lived a prosper era.\n";
        }

        else
        {
            if (cityStatus.BusinessUnbalance >= GameRules.totalInfestation)
                message = "Too many greedy businessmen came into your city,\n the excessive industry destroyed it.\n\n";

            if (cityStatus.CriminalUnbalance >= GameRules.totalInfestation)
                message = "Too many criminals came into your city,\n  your city has been spoiled and destroyed.\n\n";

            if (cityStatus.IndianUnbalance >= GameRules.totalInfestation)
                message = "Too many indians came into your city and they took the control of it.\n\n";

            if (cityStatus.MinerUnbalance >= GameRules.totalInfestation)
                message = "Too many miners came into your city.\n  The city became too noisy and unconfortable.\n\n";
                message = message+ "Periods survived: "+survivedPeriods + "/" + GameRules.totalPeriods + "\n\n";
        }

        message = message + "Times sheriff has changed: " + timesSheriffChanged + "\n";

        message = message + "Total city population: " + cityStatus.TotalPopulation + "\n";

        int totalScore = (int)calculateTotalScore(timesSheriffChanged, cityStatus);

        message = message + "Your total score: " + totalScore + "\n";


        message = message + "\n";
        message = message+ "press 'shoot' to restart\n";

        text.text = message;

    }

    private static float calculateTotalScore(int timesSheriffChanged, CityStatus cityStatus)
    {
        float totalScore = cityStatus.TotalPopulation/
                           (1 + cityStatus.BusinessUnbalance + cityStatus.CriminalUnbalance + cityStatus.IndianUnbalance +
                            cityStatus.MinerUnbalance);
        totalScore = totalScore- timesSheriffChanged;
        totalScore = ((int)totalScore)* 100;
        return totalScore;
    }

    /*
    public static void setStatistics(string cityEndInfo, int timesSheriffChanged, int businessManPopulation,
        int minerPopulation, int criminalPopulation, int indianPopulation, string playstyle, string rank)
    {
        text.text = "STATISTICS\n" +
                    cityEndInfo + "\n" +
                    "Times sheriff has changed: " + timesSheriffChanged + "\n" +
                    "Population:" +
                    "\n\tBusinessmen: " + businessManPopulation + "\n" +
                    "\n\tMiners: " + minerPopulation + "\n" +
                    "\n\tCriminals: " + criminalPopulation + "\n" +
                    "\n\tIndians:" + indianPopulation + "\n" +
                    "\n" +
                    "Your playstyle has been: " + playstyle + "\n" +
                    "Rank: " + rank;
            ;
        // your city has burnt
        //      Your city has lived an era full of crime
        //      Your city has lived a prospera era
        // times the sheriff has changed: n
        // population:
        //      - Businessman
        //      - ...
        // [Your playstyle has been: xxx]
        // rank: ***** (0/5 sheriff stars)
    }

    */

}
