using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public Guid Guid;
    public string Name;
    public Dictionary<string, string> Property = new Dictionary<string, string>();

    /// <summary>
    /// item was trash and drop satisfaction of customer
    /// </summary>
    public bool WasTrash;

    public List<ItemInfo> ItemInfos = new List<ItemInfo>();
}

public class PropertyItem
{
    public ItemInfo GeneratedItem;

    public GameObject Prototype;
    public Sprite Appearance;
}