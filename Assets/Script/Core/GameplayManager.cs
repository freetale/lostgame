using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public DailyScore Today;
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