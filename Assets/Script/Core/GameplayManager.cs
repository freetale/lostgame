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

    private void Awake()
    {
        Instance = this;
    }

    public InspectPopup InspectPopup;

    public void InspectItem(ItemPrototype item)
    {
        Debug.Log("Inspecting" + item);
        InspectPopup.Bind(item);
        if (!InspectPopup.IsOpen)
        {
            InspectPopup.Open();
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
    public List<GeneratedItem> ItemInSlot;
    public List<GeneratedItem> ItemInTrash;
    public int Today;
}

public struct DailyScore
{
    public int ImposterCount;
    public int TotalCustomer;
    public int MistakeCount;
    public int PendingItemCount;
    public int Satisfaction;
}