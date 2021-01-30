using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public DailyScore Today;

    public static GameplayManager Instance { get; private set; }

    public AudioSource SFXSource;
    
    public ItemPool ItemPool;

    public ItemListAsset ItemListAsset;
    public CharacterListAsset CharacterInfoAsset;

    public CharacterControlGroup CharacterControlGroup;

    public Randomizer Randomizer { get; private set; }

    private SaveData SaveData;

    public Transform SpawnLocation;
    public float SpawnSpaceX;

    public TimerUI TimerUI;

    public float SecondPerDay = 60;

    private float currentTime;

    private bool IsPlaying;
    private bool IsVisiting;

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

    private void ShowEndDayUI()
    {

    }

    private void ResetDay()
    {
        currentTime = SecondPerDay;
        IsPlaying = true;
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