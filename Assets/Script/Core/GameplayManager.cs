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
    public CharacterListAsset CharacterInfoAsset;

    public CharacterControlGroup CharacterControlGroup;

    public Randomizer Randomizer { get; private set; }

    private SaveData SaveData;

    [Header("Component")]
    public AudioSource SFXSource;
    public TimerUI TimerUI;
    public PoliceCall PoliceCall;
    public TalkComputer TalkComputer;
    public UIManager UIManager;

    [Header("Spawn")]
    public Transform SpawnLocation;
    public float SpawnSpaceX;
    public float SecondPerDay = 60;
    private float currentTime;

    private bool IsPlaying;
    private bool IsVisiting;

    private List<CharacterInfo> TodayCustomer = new List<CharacterInfo>();
    private List<CharacterInfo> YesterDayCustomer = new List<CharacterInfo>();
    
    private void Awake()
    {
        Instance = this;
        Randomizer = new Randomizer();
        Randomizer.ItemList = ItemListAsset.ItemList;

        for (int i = 0; i < 5; i++)
        {
            var prototype = CreateItem();
            var position = SpawnLocation.position;
            position.x += i * SpawnSpaceX;
            prototype.UpdatePosition(position);
        }

        PoliceCall.OnInteract = () => UIManager.CallForPolicePopup.Toggle();
        TalkComputer.OnInteract = () => UIManager.QuationPopup.Toggle();
        UIManager.CallForPolicePopup.OnCallPolice = OnCallPolice;
        UIManager.QuationPopup.Action = OnQuation;
        UIManager.SetTalkText("Nani");
    }

    private void Start()
    {
        ResetDay();
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

    private void CustomerExit()
    {
        IsVisiting = false;
        if (!IsPlaying)
        {
            ShowEndDayUI();
        }
    }
    private void OnCallPolice()
    {

    }

    private void OnQuation(QuationAction obj)
    {

    }

    [NaughtyAttributes.Button]
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

    private ItemPrototype CreateItem()
    {
        var generate = Randomizer.PickOne();
        var propertyItem = Randomizer.MatchProperty(generate);
        var prototype = ItemPool.PickOne();
        prototype.Bind(propertyItem);
        return prototype;
    }

    public InspectPopup InspectPopup;

    public void InspectItem(ItemPrototype item)
    {
        Debug.Log("Inspecting" + item);
        if (item != null)
        {
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