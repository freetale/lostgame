using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemListAsset : ScriptableObject
{
    public ItemList ItemList;
}

[Serializable]
public class ItemList
{
    public Item[] Items;
}

[Serializable]
public class Item
{
    public string Name;
    public ItemProperty ItemProperty;
}


[Serializable]
public class ItemProperty
{
    public string Key;
    public string[] Values;
}
