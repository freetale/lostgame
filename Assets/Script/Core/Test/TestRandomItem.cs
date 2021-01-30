﻿using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestRandomItem : MonoBehaviour
{
    public ItemListAsset ItemListAsset;

    public Randomizer ItemRandomizer;

    [Button]
    public void TestRandomOne()
    {
        ItemRandomizer = new Randomizer();
        ItemRandomizer.ItemList = ItemListAsset.ItemList;
        var item = ItemRandomizer.PickOne();
        Debug.Log(GetItemName(item));
    }

    private string GetItemName(ItemInfo item)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Name: ");
        sb.Append(item.Name);
        sb.Append(" Property: ");
        foreach (var property in item.Property)
        {
            sb.Append(property.Key);
            sb.Append(":");
            sb.Append(property.Value);
            sb.Append(",");
        }
        sb.Append("GUID:");
        sb.Append(item.Guid);
        return sb.ToString();
    }
}
