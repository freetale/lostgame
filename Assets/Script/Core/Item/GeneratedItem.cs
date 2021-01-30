using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public Guid Guid;
    public string Name;
    public string Room;
    public string Location;
    public Dictionary<string, string> Property = new Dictionary<string, string>();

    /// <summary>
    /// item was trash and drop satisfaction of customer
    /// </summary>
    public bool WasTrash;

    public List<ItemInfo> ItemInfos = new List<ItemInfo>();

    public ItemInfo CloneWithNewID()
    {
        var clone = Clone();
        clone.Guid = Guid.NewGuid();
        return clone;
    }

    public ItemInfo Clone()
    {
        var info = new ItemInfo()
        {
            Guid = Guid,
            Name = Name,
            Room = Room,
            Location = Location,
            Property = new Dictionary<string, string>(Property),
        };
        return info;
    }
}

public class PropertyItem
{
    public ItemInfo GeneratedItem;

    public GameObject Prototype;
    public Sprite Appearance;

    public string Room;
    public string Location;
}