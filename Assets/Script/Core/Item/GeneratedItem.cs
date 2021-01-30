﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class ItemInfo
{
    public Guid Guid;
    public string Name;
    public string Room;
    public string Location;
    public int Date;
    public Dictionary<string, string> Property = new Dictionary<string, string>();

    /// <summary>
    /// item was trash and drop satisfaction of customer
    /// </summary>
    public bool WasTrash;

    public List<ItemInfo> SubItem { get; } = new List<ItemInfo>();

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
            Date = Date,
            Location = Location,
            Property = new Dictionary<string, string>(Property),
        };
        info.SubItem.AddRange(SubItem.Select(i => i.Clone()));
        return info;
    }

    public string GetDescriptionString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in Property)
        {
            sb.Append(item.Value);
            sb.Append(" ");
        }
        sb.Append(Name);
        return sb.ToString();
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