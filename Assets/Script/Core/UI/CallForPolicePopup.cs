using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallForPolicePopup : PanelBehaviour
{
    public Action OnCallPolice;

    public Button CallButton;

    private void Awake()
    {
        CallButton.onClick.AddListener(CallButton_OnClick);
    }

    private void CallButton_OnClick()
    {
        OnCallPolice();
    }
}
