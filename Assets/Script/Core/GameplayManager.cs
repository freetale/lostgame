using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public const int MaxStatisfaction = 10;
    public DailyScore Today;
    [NonSerialized]
    public List<DailyScore> AllDays = new List<DailyScore>();
    public static GameplayManager Instance { get; private set; }

    public ItemPool ItemPool;
    [Header("Asset")]
    public ItemListAsset ItemListAsset;
    public CharacterListAsset CharacterListAsset;
    public TalkScriptAsset TalkScriptAsset;

    public CharacterControlGroup CharacterControlGroup;

    public Randomizer Randomizer { get; private set; }

    private SaveData SaveData;

    [Header("Component")]
    public PickupManager PickupManager;
    public AudioSource SFXSource;
    public TimerUI TimerUI;
    public PoliceCall PoliceCall;
    public TalkComputer TalkComputer;
    public UIManager UIManager;
    public InspectPopup InspectPopup;
    public ItemSpawner ItemSpawner;

    public float SecondPerDay = 60;
    public float DialogueInterval = 3f;
    [ShowNonSerializedField]
    private float currentTime;

    [ShowNonSerializedField]
    private bool IsOpenTime;
    [ShowNonSerializedField]
    private bool IsVisiting;

    [NonSerialized]
    private List<CharacterInfo> TodayCustomer = new List<CharacterInfo>();
    [NonSerialized]
    private List<CharacterInfo> YesterDayCustomer = new List<CharacterInfo>();
    [NonSerialized]
    private List<CharacterInfo> WaitingCustomer = new List<CharacterInfo>();

    [NonSerialized]
    private List<ItemPrototype> StandAloneItem = new List<ItemPrototype>();
    [NonSerialized]
    private CharacterInfo CurrentCustomer;

    [ShowNativeProperty]
    public int StandAloneCount => StandAloneItem.Count;

    [ShowNonSerializedField]
    private int CurrentDayIndex = 0;

    private SessionRandom SessionRandom;

    [Scene]
    public string MainMenu;

    private void Awake()
    {
        Instance = this;
        Randomizer = new Randomizer();
        Randomizer.ItemList = ItemListAsset.ItemList;
        Randomizer.CharacterListAsset = CharacterListAsset;


        PoliceCall.OnInteract = () => UIManager.CallForPolicePopup.Toggle();
        TalkComputer.OnInteract = () => UIManager.QuationPopup.Toggle();
        UIManager.CallForPolicePopup.OnCallPolice = OnCallPolice;
        UIManager.QuationPopup.Action = OnQuation;
        UIManager.OnNextDay = OnNextDay;
        PickupManager.OnPickup = OnPickUp;
        PickupManager.OnDropdown = OnDropDown;
        ItemSpawner.OnSpawn = OnSpawn;
        TalkScriptAsset = new TalkScriptAsset();
    }

    private void Start()
    {
        ResetDayTimer();
        SessionRandom = Randomizer.GetSessionRandom();
        UpdateDay(0);
        CustomerComing();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                OnDayEnd();
            }
            float normalizeTime = currentTime / SecondPerDay;
            TimerUI.SetTime(normalizeTime);
        }
    }

    private void OnDayEnd()
    {
        Debug.Log("DayEnd");
        IsOpenTime = false;
        if (!IsVisiting)
        {
            ShowEndDayUI();
        }
    }

    private void UpdateDay(int index)
    {
        CurrentDayIndex = index;
        var todayRandom = SessionRandom.Days[index];
        YesterDayCustomer.Clear();
        YesterDayCustomer.AddRange(TodayCustomer);
        TodayCustomer.Clear();
        TodayCustomer.AddRange(todayRandom.Characters);
        ItemSpawner.Spawn(todayRandom.ItemInfo);
    }

    private void CustomerComing()
    {
        if (TodayCustomer.Count == 0)
        {
            if (YesterDayCustomer.Count == 0)
            {
                Debug.Log("No more customer");
            }
            else
            {
                SummonCustomer(YesterDayCustomer[0]).Forget();
                YesterDayCustomer.RemoveAt(0);
            }
        }
        else
        {
            SummonCustomer(TodayCustomer[0]).Forget();
            TodayCustomer.RemoveAt(0);
        }
    }

    private void OnSpawn(ItemPrototype obj)
    {
        StandAloneItem.Add(obj);
        UIManager.SetQuationSubmitItem(StandAloneItem.Count == 1);
    }

    private async UniTask SummonCustomer(CharacterInfo characterInfo)
    {
        IsVisiting = true;
        CurrentCustomer = characterInfo;
        CharacterControlGroup.Bind(characterInfo);
        await CharacterControlGroup.Play(CharacterAnimation.MoveIn);
        UIManager.SetTalkText(string.Format(TalkScriptAsset.InComing, characterInfo.LookingForItem.Name, characterInfo.Name));
    }

    private void OnCharacterExit()
    {
        CurrentCustomer = null;
        IsVisiting = false;
        if (!IsOpenTime)
        {
            ShowEndDayUI();
        }
        else
        {
            CustomerComing();
        }
    }

    private void OnCallPolice()
    {
        if (CurrentCustomer == null)
        {
            return;
        }
        CallPolice().Forget();
    }

    private async UniTask CallPolice()
    {
        var isThift = CurrentCustomer.IsImposter;
        CurrentCustomer = null;
        UIManager.HideUserUI();
        if (isThift) Today.ImposterCapture++;
        else Today.InnocentCapture++;
        await PoliceComingAsync(isThift);
        OnCharacterExit();
    }

    private async UniTask PoliceComingAsync(bool isThift)
    {
        await CharacterControlGroup.Play(CharacterAnimation.PoliceMoveIn);
        UIManager.SetPoliceText(TalkScriptAsset.PoliceArrsting);
        await UniTask.Delay(TimeSpan.FromSeconds(DialogueInterval));
        if (isThift)
        {
            UIManager.SetTalkText(TalkScriptAsset.ThiftArrest.PickRandom());
            await UniTask.Delay(TimeSpan.FromSeconds(DialogueInterval / 2));
            await CharacterControlGroup.Play(CharacterAnimation.PoliceMoveout);
        }
        else
        {
            UIManager.SetTalkText(TalkScriptAsset.WrongArrest1);
            await UniTask.Delay(TimeSpan.FromSeconds(DialogueInterval));
            UIManager.SetPoliceText(TalkScriptAsset.WrongArrest2Police);
            await UniTask.Delay(TimeSpan.FromSeconds(DialogueInterval));
            UIManager.SetTalkText(TalkScriptAsset.WrongArrest3);
            await CharacterControlGroup.Play(CharacterAnimation.PoliceMoveout);
        }
    }
    public void OnNextDay()
    {
        CurrentDayIndex++;
        UIManager.EndDayUI.Close();
        if (CurrentDayIndex == 2)
        {
            DailyScore total = new DailyScore();
            for (int i = 0; i < AllDays.Count; i++)
            {
                DailyScore score = AllDays[i];
                total.TotalCustomer = score.TotalCustomer;
                total.ImposterCapture = score.ImposterCapture;
                total.ImposterGetAway = score.ImposterGetAway;
                total.InnocentCapture = score.InnocentCapture;
                total.CustomerStilMissing = score.CustomerStilMissing;
                total.MaxSatisfaction = score.MaxSatisfaction;
                total.Satisfaction = score.Satisfaction;
            }
            UIManager.EndWeek(total);
        }
        else if(CurrentDayIndex == 3) // exit
        {
            SceneManager.LoadScene(MainMenu);
        }
        else
        {
            AllDays.Add(Today);
            Today = new DailyScore();
            ResetDayTimer();
            UpdateDay(CurrentDayIndex);
            CustomerComing();
        }
    }

    private void OnQuation(QuationAction quation)
    {
        if (CurrentCustomer == null)
            return;
        var looking = CurrentCustomer.LookingForItem;
        var today = looking.Date == CurrentDayIndex;
        var text = GetQuation(quation, looking, today, CurrentCustomer.IsImposter);
        UIManager.SetTalkText(text);
        if (quation != QuationAction.HereYouAre)
        {
            CurrentCustomer.Satisfaction = Mathf.Max(CurrentCustomer.Satisfaction - 1, 0);
        }
        if (quation == QuationAction.CameBackTomorrow)
        {
            CustommerComeTomorrow().Forget();
        }
    }

    private string GetQuation(QuationAction quation, ItemInfo looking, bool today, bool isImposter)
    {
        switch (quation)
        {
            case QuationAction.WhichRoom:
                if (looking.Room == null)
                    return TalkScriptAsset.DontKnow;
                return string.Format(TalkScriptAsset.WhichRoom, looking.Room);
            case QuationAction.LostDate:
                if (today)
                    return TalkScriptAsset.DateToday;
                else
                    return TalkScriptAsset.DateYesterday;
            case QuationAction.LastSeen:
                if (looking.Room == null)
                    return TalkScriptAsset.DontKnow;
                return string.Format(TalkScriptAsset.Where, looking.Location);
            case QuationAction.WhatLostItem:
                if (looking.SubItem.Count > 0)
                {
                    ItemInfo sub = looking.SubItem.PickRandom();
                    return string.Format(TalkScriptAsset.WhatItemMultiple, looking.GetDescriptionString(), sub.GetDescriptionString());
                }
                else
                {
                    return string.Format(TalkScriptAsset.WhatItemSingle, looking.GetDescriptionString());
                }
            case QuationAction.HereYouAre:
                if (StandAloneItem.Count != 1)
                {
                    Debug.LogWarning("No avaliable item");
                    return "";
                }
                if (isImposter)
                {
                    ImposterTakeAway().Forget();
                    return string.Format(TalkScriptAsset.Thanks);
                }
                var guid = StandAloneItem[0].ItemInfo.Guid;
                if (looking.Guid == guid)
                {
                    CustomerTakeAway().Forget();
                    return string.Format(TalkScriptAsset.Thanks);
                }
                else return string.Format(TalkScriptAsset.WrongItem);
            case QuationAction.CameBackTomorrow:
                return string.Format(TalkScriptAsset.ComeBackTomorrow);
            default:
                return "";
        }
    }

    private async UniTask CustommerComeTomorrow()
    {
        UIManager.HideUserUI();
        WaitingCustomer.Add(CurrentCustomer);
        Today.TotalCustomer++;
        //Today.MaxSatisfaction += MaxStatisfaction;
        //Today.Satisfaction += CurrentCustomer.Satisfaction;

        CurrentCustomer = null;
        await UniTask.Delay(TimeSpan.FromSeconds(DialogueInterval));
        await CharacterControlGroup.Play(CharacterAnimation.MoveOut);
        UIManager.ClearTalkText();
        OnCharacterExit();
    }

    private async UniTask CustomerTakeAway()
    {
        UIManager.HideUserUI();
        Today.TotalCustomer++;
        Today.MaxSatisfaction += MaxStatisfaction;
        Today.Satisfaction += CurrentCustomer.Satisfaction;

        CurrentCustomer = null;
        var trashed = ReturnOneItemToPool();
        CharacterControlGroup.SetEmoticon(trashed ? Emotion.Angry : Emotion.Happy);
        await CharacterControlGroup.Play(CharacterAnimation.MoveOut);
        UIManager.ClearTalkText();
        OnCharacterExit();
    }

    private async UniTask ImposterTakeAway()
    {
        UIManager.HideUserUI();
        CurrentCustomer = null;
        Today.ImposterGetAway++;

        var trashed =  ReturnOneItemToPool();
        CharacterControlGroup.SetEmoticon(trashed ? Emotion.Angry : Emotion.Happy);
        await CharacterControlGroup.Play(CharacterAnimation.MoveOut);
        UIManager.ClearTalkText();
        await UIManager.ShowBossMessage(TalkScriptAsset.BossThiftNotify);
        OnCharacterExit();
    }

    private bool ReturnOneItemToPool()
    {
        if (StandAloneItem.Count != 1)
        {
            throw new ArgumentException("No avaliable item");
        }
        var item = StandAloneItem[0];
        StandAloneItem.RemoveAt(0);
        ItemPool.Return(item);
        UIManager.SetQuationSubmitItem(StandAloneItem.Count == 1);
        return item.ItemInfo.WasTrash;
    }

    private void ShowEndDayUI()
    {
        UIManager.EndDay(Today);
    }

    private void ResetDayTimer()
    {
        currentTime = SecondPerDay;
        IsOpenTime = true;
        IsVisiting = false;
    }

    private void OnPickUp(ItemPrototype item)
    {
        if (item.AttachTo != null)
        {
            item.AttachTo.ItemLeave(item);
            item.OnLeaveSlot();
            item.AttachTo = null;
        }
        item.OnPick();
        if (!StandAloneItem.Contains(item))
        {
            StandAloneItem.Add(item);
            UIManager.SetQuationSubmitItem(StandAloneItem.Count == 1);
        }
    }

    private void OnDropDown(ItemPrototype item, IDropItemable reciver)
    {
        if (reciver != null && reciver.IsEmpty && reciver.CanDrop(item))
        {
            StandAloneItem.Remove(item);
            UIManager.SetQuationSubmitItem(StandAloneItem.Count == 1);
            item.AttachTo = reciver;
            item.OnDrop();
            item.OnEnterSlot();
            reciver.ItemEnter(item);
        }
        else
        {
            item.OnDrop();
        }
    }


    public void InspectItem(ItemPrototype item)
    {
        if (item != null)
        {
            Debug.Log("Inspecting" + item.ItemInfo.GetDescriptionString());
            UIManager.OpenDescription(item.ItemInfo);
            //InspectPopup.Bind(item);
            //if (!InspectPopup.IsOpen)
            //{
            //    //InspectPopup.Open(); // not allow to inspect
            //}
        }
        else
        {
            //if (InspectPopup.IsOpen)
            //{
            //    UIManager.CloseDescription();
            //    InspectPopup.Close();
            //}
        }
    }

    public void PlayerSfx(AudioClip clip)
    {
        if (clip != null)
            SFXSource.PlayOneShot(clip);
    }
}

public struct SaveData
{
    public List<DailyScore> DailyScores;
    public List<ItemInfo> ItemInSlot;
    public List<ItemInfo> ItemInTrash;
    public int Today;
}

