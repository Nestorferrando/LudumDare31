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

    private static Dictionary<Role, string[]> dialogues = new Dictionary<Role, string[]>();
    private static Random  randomValue = new Random();

    private static void init()
    {
        dialogues.Add(Role.BusinessMan, bussinessMan);
        dialogues.Add(Role.Criminal, criminal);
        dialogues.Add(Role.Indian, indian);
        dialogues.Add(Role.Miner, miner);
    }

    public static string getDialog(Role r)
    {
        if (dialogues.Count == 0)
        {
            init();
        }

        String[] value;
        dialogues.TryGetValue(r, out value);

        int i = randomValue.Next(0, value.Length);
        return value[i];
    }
 

}
