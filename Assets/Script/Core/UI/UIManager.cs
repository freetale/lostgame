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
    public TalkPopup PolicePopup;
    public DescriptionPopup DescriptionPopup;

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

    public void SetPoliceText(string v)
    {
        PolicePopup.SetText(v);
        PolicePopup.WaitAndClose();
    }

    internal void EndDay(DailyScore score)
    {
        EndDayUI.SetInfomation(score);
        EndDayUI.Open();
    }

    public void OpenDescription(ItemInfo item)
    {
        DescriptionPopup.SetItem(item);
        DescriptionPopup.Open();
    }
    public void CloseDescription()
    {
        DescriptionPopup.Close();
    }

    internal void HideUserUI()
    {
        PolicePopup.Close();
        TalkPopup.Close();
        DescriptionPopup.Close();
    }
}
