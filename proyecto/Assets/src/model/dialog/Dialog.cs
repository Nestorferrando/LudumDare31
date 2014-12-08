using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = System.Random;

public class Dialog
{

    private static string[] bussinessMan =
    {
        "This place looks GREAT for my bussiness",
        "I am the greatest and the goldest",
        "My viscera are made of money",
        "Whatever I touch it gets a dollar somewhere",
        "You can't say 'No' to money. Can you, Sheriff?",
        "Sorry sheriff, but I can't tell you what's inside my suitcase"
    };

    private static string[] miner =
    {
        "Gold, gold, gold, gold, gold!",
        "I heard this city has a gold mine in here!",
        "My family is starving, I hope I can get gold out the mine of this city",
        "My friends call me goldier because I make gold out of nowhere",
        "I've been travelling for ages just to get here and fill my mouth with GOLD",
        "A saloon and a mine, what else can a man desire?"
    };

    private static string[] criminal =
    {
        "I smell LADIES here",
        "Where's the saloon? I'm going to stay there every day of my life",
        "I have to kill that bastard of Snowman",
        "What's up sheriff? A cat got your tongue?",
        "Time to get some fun here. YIHAAA!",
        "I hope I can find my last partner here. I have some unfinished bussiness with him"
    };

    private static string[] indian =
    {
        "You've destroyed my land and now I have nowhere else to live but this city",
        "I hate my tribe, I wanna be just like you when I grow old",
        "My ancient ancestors lived for centuries here and I wanna be with them",
        "This place looks nice for building my tipi. I hope you guys don't get annoyed",
        "I need to stay in this city for a week or two, it won't be long sheriff",
        "You've no soul, but my tribe has no ladies"
    };

    private static string[] toggleSheriff =
    {
        "Now I'm the sheriff! HA!",
        "He deserved it. Now I am the sheriff!",
        "Nobody tells me what to do. I think that star will fit me well.",
        "This star is so shiny it has to be mine!"
    };

    private static string[] sheriff =
    {
        "We don't need people like you arround!",
        "We already have too many of your type here!",
        "Leave! this town will not be your home!",
        "This town has already too many people like you!. Go away!",
        "Stay away from my city, you scum!",
        "This city is not big enough for people like you",
        "I am the fastest shooter in here, so don't even try to kill me!",
        "Do you think I am stupid? I'm not letting you in!"
    };

    private static string[] sheriffSurrender =
    {
        "I surrender!",
        "I have no more bullets! please don't kill me!",
        "I'm unarmed, have mercy!",
        "Don't Shoot, I'm Unarmed!",
        "Please, don't shoot!",
        "Like if I had a choice"
    };

    private static string[] sheriffAccepts =
    {
        "Great! Wellcome to Ragtown",
        "We totally need more people like you in our town",
        "Wellcome to your new home",
        "The more the merrier",
        "Come in!",
        "Wow, you look awesome, please join us",
        "Please, please, wellcome",
        "I can't say no to your sincere request"
    };


    private static Dictionary<Role, string[]> dialogues = new Dictionary<Role, string[]>();
    private static Random  randomValue = new Random();

    private static void init()
    {
        dialogues.Add(Role.BusinessMan, bussinessMan);
        dialogues.Add(Role.Criminal, criminal);
        dialogues.Add(Role.Indian, indian);
        dialogues.Add(Role.Miner, miner);
    }

    public static string getWaveDialog(Role r)
    {
        if (dialogues.Count == 0)
        {
            init();
        }

        String[] value;
        dialogues.TryGetValue(r, out value);

        int i = randomValue.Next(0, value.Length);
        return value[i].ToUpper();
    }

    public static string getSheriffDiedDialog()
    {
        int i = randomValue.Next(0, toggleSheriff.Length);
        return toggleSheriff[i].ToUpper();
    }

    public static string getSheriffRejectDialog()
    {
        int i = randomValue.Next(0, sheriff.Length);
        return sheriff[i].ToUpper();
    }

    public static string getSheriffSurrenderDialog()
    {
        int i = randomValue.Next(0, sheriffSurrender.Length);
        return sheriffSurrender[i].ToUpper();
    }

    public static string getSheriffAcceptsDialog()
    {
        int i = randomValue.Next(0, sheriffAccepts.Length);
        return sheriffAccepts[i].ToUpper();
    }
 

}
