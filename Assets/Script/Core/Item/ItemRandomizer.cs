using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemRandomizer
{
    public ItemList ItemList;

    public GeneratedItem PickOne()
    {
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
        
        return generated;
    }

    public PropertyItem MatchPrototype(GeneratedItem generated)
    {
        var item = ItemList.Items.First(i => i.Name == generated.Name);
        PropertyItem property = new PropertyItem();
        property.GeneratedItem = generated;
        property.Prototype = item.Prototype;
        string propertyAppearance = generated.Property[item.AppearancesKey];
        property.Appearance = item.ItemAppearances.First(i => i.Value == propertyAppearance).Sprite;
        return property;
    }
}
