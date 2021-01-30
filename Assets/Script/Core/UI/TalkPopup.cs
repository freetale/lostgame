﻿using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkPopup : PanelBehaviour
{
    public TMP_Text Text;

    public float CloseDelay = 3f;

    private float CloseTime;

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void WaitAndClose()
    {
        CloseTime = CloseDelay + Time.time;
        if (!IsOpen)
        {
            Open();
            WaitAndCloseAsync();
        }
    }

    private async void WaitAndCloseAsync()
    {
        await UniTask.WaitUntil(() => CloseTime < Time.time);
        Close();
    }

    public override UniTask CloseAsync()
    {
        CloseTime = Time.time - 1;
        return base.CloseAsync();
    }
}
