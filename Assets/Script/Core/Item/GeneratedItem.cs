using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
    public string OwnerName;
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
            var value = item.Value;
            //Spaciala value
            if (value == "#NAME#")
            {
                value = OwnerName;
            }
            sb.Append(value);
            sb.Append(" ");
        }
        sb.Append(Name);
        return sb.ToString();
    }

    public string GetInfoString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(Name);
        sb.AppendFormat("Found: {0} {1}", Room, Location);
        sb.AppendLine();
        bool firstProp = true;
        foreach (var item in Property)
        {
            if (firstProp)
            {
                firstProp = false;
            }
            else
            {
                sb.Append(", ");
            }
            var value = item.Value;
            //Spaciala value
            if (value == "#NAME#")
            {
                value = OwnerName;
            }
            sb.AppendFormat("{0}-{1}", item.Key, value);
        }
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