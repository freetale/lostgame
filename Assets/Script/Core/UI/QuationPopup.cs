using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuationAction
{
    WhichRoom,
    LostDate,
    LastSeen,
    WhatLostItem,
    [Obsolete]
    ThisOne,
    HereYouAre,
    CameBackTomorrow,
}

public class QuationPopup : PanelBehaviour
{
    public Button WhatYourRoomButton;
    public Button LostDateButton;
    public Button LastSeenButton;
    public Button WhatLostItemButton;
    public Button ThisOneButton;
    public Button HereYouAreButton;
    public Button ComebackTomorrowButton;

    public Action<QuationAction> Action;

    private void Awake()
    {
        WhatYourRoomButton.onClick.AddListener(WhatYouRoom_OnClick);
        LostDateButton.onClick.AddListener(LostDate_OnClick);
        LastSeenButton.onClick.AddListener(LastSeen_OnClick);
        WhatLostItemButton.onClick.AddListener(WhatLostItem_OnClick);
        ThisOneButton.onClick.AddListener(ThisOne_OnClick);
        HereYouAreButton.onClick.AddListener(HereYouAre_OnClick);
        ComebackTomorrowButton.onClick.AddListener(ComebackTomorrow_OnClick);
    }

    private void WhatYouRoom_OnClick()
    {
        Action(QuationAction.WhichRoom);
    }

    private void LostDate_OnClick()
    {
        Action(QuationAction.LostDate);
    }

    private void LastSeen_OnClick()
    {
        Action(QuationAction.LastSeen);
    }

    private void WhatLostItem_OnClick()
    {
        Action(QuationAction.WhatLostItem);
    }

    private void ThisOne_OnClick()
    {
        Action(QuationAction.ThisOne);
    }

    private void HereYouAre_OnClick()
    {
        Action(QuationAction.HereYouAre);
    }

    private void ComebackTomorrow_OnClick()
    {
        Action(QuationAction.CameBackTomorrow);
    }

    public void SetSubmitItem(bool item) 
    {
        ThisOneButton.interactable = item;
        HereYouAreButton.interactable = item;
    }
}
