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

    public Randomizer Randomizer { get; private set; }

    private SaveData SaveData;

    public Transform SpawnLocation;
    public float SpawnSpaceX;

    private void Awake()
    {
        Instance = this;
        Randomizer = new Randomizer();
        Randomizer.ItemList = ItemListAsset.ItemList;

        for (int i = 0; i < 5; i++)
        {
            var prototype =  CreateItem();
            var position = SpawnLocation.position;
            position.x += i * SpawnSpaceX;
            prototype.UpdatePosition(position);
        }
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
    public int ImposterCount;
    public int TotalCustomer;
    public int MistakeCount;
    public int PendingItemCount;
    public int Satisfaction;
}