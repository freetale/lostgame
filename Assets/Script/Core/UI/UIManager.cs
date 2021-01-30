using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public EndDayUI EndDayUI;
    public CallForPolicePopup CallForPolicePopup;
    public QuationPopup QuationPopup;
    public TalkPopup TalkPopup;

    public Action OnNextDay;

    private void Awake()
    {
        EndDayUI.OnNextDay = OnNextDay;
    }

    public void SetTalkText(string v)
    {
        TalkPopup.SetText(v);
        TalkPopup.WaitAndClose();
    }

    internal void EndDay(DailyScore score)
    {
        EndDayUI.SetInfomation(score);
        EndDayUI.Open();
    }
}
