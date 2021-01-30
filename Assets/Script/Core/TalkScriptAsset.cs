﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScriptAsset
{
    /// <summary>
    /// {0} for item name, {1] name
    /// </summary>
    public string InComing = "Hello I’m {1} Have you found my {0}";

    /// <summary>
    /// 0 for item name and property, 1 for sub item name
    /// </summary>
    public string DescriptItem = "It was {0} with my {1}";

    /// <summary>
    /// lost date
    /// </summary>
    public string DateToday = "At moring";

    /// <summary>
    /// lost date
    /// </summary>
    public string DateYesterday = "Yesterday";

    /// <summary>
    /// 0 for owner
    /// </summary>
    public string DontKnow = "I don’t know my {0} told me to get it";

    /// <summary>
    /// this one?
    /// </summary>
    public string CurrentItem = "Current item";

    /// <summary>
    /// this one?
    /// </summary>
    public string WrongItem = "No, not this one";
    public string ComeBackTomorrow = "I'll be back tomorrow";

    public string PoliceArrsting = "This taste… Is the taste of LIAR";

    public string[] ThiftArrest = { "That’s sexual harassment", "I hate this meme", "This must be an attack from an enemy stand!" };
    public string WrongArrest1 = "I’m not a thief";
    public string WrongArrest2Police = "I don’t care";
    public string WrongArrest3 = "NOOOOOOOO";

    public string BossNotify = "That’s was innocent customer -1 for your performance";
    public string BossThiftNotify = "That’s person a thief -1 for your performance";
}
