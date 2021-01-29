﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemRandomizer
{
    public ItemList ItemList;

    public GeneratedItem PickOne()
    {
        // TODO: filter item
        var possibleItem = ItemList.Items.Select(i => i);
        var item = possibleItem.PickRandom();
        return CreateItem(item);
    }

    private GeneratedItem CreateItem(Item item)
    {
        GeneratedItem generated = new GeneratedItem();
        generated.Name = item.Name;
        for (int i = 0; i < item.ItemProperties.Length; i++)
        {
            string key = item.ItemProperties[i].Key;
            string value = item.ItemProperties[i].Values.PickRandom();
            generated.Property.Add(key, value);
        }
        string propertyAppearance = generated.Property[item.AppearancesKey];
        generated.Appearance = item.ItemAppearances.First(i => i.Value == propertyAppearance).Sprite;
        return generated;
    }
}
