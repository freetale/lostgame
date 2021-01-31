using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public EndDayUI EndDayUI;
    public CallForPolicePopup CallForPolicePopup;
    public QuationPopup QuationPopup;
    public TalkPopup TalkPopup;
    public TalkPopup PolicePopup;
    public TalkPopup BossPopup;
    public DescriptionPopup DescriptionPopup;

    public Action OnNextDay;

    private void Awake()
    {
        EndDayUI.OnNextDay = OnNextDay;
    }

    public void SetTalkText(string v)
    {
        TalkPopup.Cancle();
        TalkPopup.SetText(v);
        TalkPopup.WaitAndClose();
    }

    public void SetPoliceText(string v)
    {
        PolicePopup.Cancle();
        PolicePopup.SetText(v);
        PolicePopup.WaitAndClose();
    }

    internal void EndDay(DailyScore score)
    {
        Debug.Log("EndDay");
        EndDayUI.SetInfomation(score);
        EndDayUI.Open();
    }

    internal void EndWeek(DailyScore total)
    {
        Debug.Log("EndWeek");
        EndDayUI.SetInfomation(total);
        EndDayUI.SetButton("Mainmenu");
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
        //PolicePopup.Close();
        //TalkPopup.Close();
        DescriptionPopup.Close();
        CallForPolicePopup.Close();
        QuationPopup.Close();
    }

    public async UniTask ShowBossMessage(string bossThiftNotify)
    {
        BossPopup.SetText(bossThiftNotify);
        BossPopup.WaitAndClose();

        await UniTask.WaitUntil(() => !BossPopup.IsOpen);
    }

    public void SetQuationSubmitItem(bool canSubmit)
    {
        QuationPopup.SetSubmitItem(canSubmit);
    }

    internal void ClearTalkText()
    {
        TalkPopup.Close();
    }
}
