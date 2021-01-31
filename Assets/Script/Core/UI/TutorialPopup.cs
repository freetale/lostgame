using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : PanelBehaviour
{
    public Button ExitButton;

    private void Awake()
    {
        ExitButton.onClick.AddListener(ExitButton_OnClick);
    }

    private void ExitButton_OnClick()
    {
        Close();
    }
}
