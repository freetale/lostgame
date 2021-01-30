using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public DailyScore Today;

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
    private float currentTime;

    private bool IsPlaying;
    private bool IsVisiting;

    [NonSerialized]
    private List<CharacterInfo> TodayCustomer = new List<CharacterInfo>();
    [NonSerialized]
    private List<CharacterInfo> YesterDayCustomer = new List<CharacterInfo>();
    [NaughtyAttributes.ShowNonSerializedField]
    private List<ItemPrototype> StandAloneItem = new List<ItemPrototype>();
    [NonSerialized]
    private CharacterInfo CurrentCustomer;


    [NaughtyAttributes.ShowNonSerializedField]
    private int CurrentDayIndex = 0;

    private SessionRandom SessionRandom;

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
        PickupManager.OnPickup = OnPickUp;
        PickupManager.OnDropdown = OnDropDown;
        ItemSpawner.OnSpawn = OnSpawn;
        TalkScriptAsset = new TalkScriptAsset();
    }

    private void Start()
    {
        ResetDay();
        SessionRandom = Randomizer.GetSessionRandom();
        UpdateDay(0);
        CustomerComing();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime == 0)
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
        IsPlaying = false;
        if (!IsVisiting)
        {
            ShowEndDayUI();
        }
    }

    private void UpdateDay(int index)
    {
        CurrentDayIndex = index;
        var todayRandom = SessionRandom.Days[index];
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
    }

    private async UniTask SummonCustomer(CharacterInfo characterInfo)
    {
        CurrentCustomer = characterInfo;
        CharacterControlGroup.Bind(characterInfo);
        await CharacterControlGroup.Play(CharacterAnimation.MoveIn);
        UIManager.SetTalkText(string.Format(TalkScriptAsset.InComing, characterInfo.LookingForItem.Name, characterInfo.Name));
    }

    private void CustomerExit()
    {
        CurrentCustomer = null;
        IsVisiting = false;
        if (!IsPlaying)
        {
            ShowEndDayUI();
        }
    }
    private void OnCallPolice()
    {
        if (CurrentCustomer == null)
        {
            return;
        }
        var isThift = CurrentCustomer.IsImposter;
        CurrentCustomer = null;
        IsVisiting = false;
        UIManager.HideUserUI();
        PoliceComingAsync(isThift).Forget();
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

    private void OnQuation(QuationAction quation)
    {
        if (CurrentCustomer == null)
            return;

    }

    private void ShowEndDayUI()
    {
        UIManager.EndDay(Today);
    }

    public void PushCustomer(CharacterInfo info)
    {
        
    }

    private void ResetDay()
    {
        currentTime = SecondPerDay;
        IsPlaying = true;
    }

    public void MoveItemTo(ItemPrototype prototype, IDropItemable itemable)
    {

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
        StandAloneItem.Add(item);
        InspectItem(null);
    }

    private void OnDropDown(ItemPrototype item, IDropItemable reciver)
    {
        if (reciver != null && reciver.IsEmpty && reciver.CanDrop(item))
        {
            StandAloneItem.Remove(item);
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
            Debug.Log("Inspecting" + item);
            UIManager.OpenDescription(item.ItemInfo);
            InspectPopup.Bind(item);
            if (!InspectPopup.IsOpen)
            {
                InspectPopup.Open();
            }
        }
        else
        {
            if (InspectPopup.IsOpen)
            {
                InspectPopup.Close();
            }
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

public struct DailyScore
{
    public int TotalCustomer;
    public int ImposterCapture;
    public int ImposterGetAway;
    public int InnocentGetAways;
    public int CustomerStilMissing;
    public int PendingItemCount;
    public int MaxSatisfaction;
    public int Satisfaction;
}