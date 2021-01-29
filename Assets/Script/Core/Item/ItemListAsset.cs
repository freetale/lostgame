using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class ItemListAsset : ScriptableObject
{
    public ItemList ItemList;
}

[Serializable]
public class ItemList
{
    [BoxGroup]
    public Item[] Items;
}

[Serializable]
public class Item
{
    [HorizontalLine]
    public string Name;
    public bool CanBeSubSet = false;
    public bool CanBeSuperSet = false;
    public bool Exclusive = false;

    [ValidateInput(nameof(IsAppearanceFine))]
    public ItemProperty[] ItemProperties;

    public string AppearancesKey;
    public ItemAppearance[] ItemAppearances;

    public bool IsAppearanceFine()
    {
        var properties = ItemProperties.First(i => i.Key == AppearancesKey);
        if (properties == null)
            return  false;
        List<string> missing = new List<string>();
        foreach (var item in properties.Values)
        {
            if (!ItemAppearances.Any(i => i.Value == item))
            {
                missing.Add(item);
            }
        }
        if(missing.Count > 0)
        {
            return false;
        }
        return true;
    }
}

[Serializable]
public class ItemProperty
{
    public string Key;
    public string[] Values;

    [Button]
    public void CreateAppearance()
    {

    }
}

[Serializable]
public class ItemAppearance
{
    public string Value;
    public Sprite Sprite;
}