using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatisticsText : MonoBehaviour {

    private static Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

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
}
