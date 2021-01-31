using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScriptAsset
{
    /// <summary>
    /// {0} for item name, {1] name
    /// </summary>
    public string InComing = "Hello I’m {1} Have you found my {0}";

    /// <summary>
    /// 0 for room number
    /// </summary>
    public string WhichRoom = "I lost them at room {0}";

    /// <summary>
    /// 0 for time
    /// </summary>
    public string WhenDidLostIt = "At {0}";

    /// <summary>
    /// 0 is location
    /// </summary>
    public string Where = "Around {0}";

    /// <summary>
    /// 0 for item and property
    /// </summary>
    public string WhatItemSingle = " It was {0}";

    /// <summary>
    /// 0 for item and property, 1 for random sub item
    /// </summary>
    public string WhatItemMultiple = " It was {0}, It has {1} inside";

    /// <summary>
    /// lost date
    /// </summary>
    public string DateToday = "Today";

    /// <summary>
    /// lost date
    /// </summary>
    public string DateYesterday = "Yesterday";

    /// <summary>
    /// 0 for owner
    /// </summary>
    public string DontKnow = "I don’t know";

    /// <summary>
    /// Here you are
    /// </summary>
    public string Thanks = "Thanks";

    /// <summary>
    /// Here you are
    /// </summary>
    public string WrongItem = "No not this one";

    public string ComeBackTomorrow = "Please find it";

    public string PoliceArrsting = "This taste… Is the taste of LIAR";

    public string[] ThiftArrest = { "That’s sexual harassment", "I hate this meme", "This must be an attack from an enemy stand!" };
    public string WrongArrest1 = "I’m not a thief";
    public string WrongArrest2Police = "I don’t care";
    public string WrongArrest3 = "NOOOOOOOO";

    public string BossInnocentNotify = "That’s was innocent customer\n-1 for your performance";
    public string BossThiftNotify = "That’s person a thief\n-1 for your performance";
}
