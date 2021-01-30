using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Randomizer
{
    public ItemList ItemList;

    public ItemInfo PickOne()
    {
        var possibleItem = ItemList.Items.Select(i => i);
        var item = possibleItem.PickRandom();
        return CreateItem(item, ItemList.Room, ItemList.Location);
    }

    private ItemInfo CreateItem(Item item, IEnumerable<string> room, IEnumerable<string> location)
    {
        ItemInfo generated = new ItemInfo();
        generated.Name = item.Name;
        generated.Guid = Guid.NewGuid();
        generated.Room = room.PickRandom();
        generated.Location = location.PickRandom();

        for (int i = 0; i < item.ItemProperties.Length; i++)
        {
            string key = item.ItemProperties[i].Key;
            string value = item.ItemProperties[i].Values.PickRandom();
            generated.Property.Add(key, value);
        }
        
        return generated;
    }

    public PropertyItem MatchProperty(ItemInfo generated)
    {
        var item = ItemList.Items.First(i => i.Name == generated.Name);
        PropertyItem property = new PropertyItem();
        property.GeneratedItem = generated;
        property.Prototype = item.Prototype;
        string propertyAppearance = generated.Property[item.AppearancesKey];
        property.Appearance = item.ItemAppearances.First(i => i.Value == propertyAppearance).Sprite;
        return property;
    }

    public CharacterInfo RandomCharacter(CharacterListAsset character)
    {
        CharacterInfo info = new CharacterInfo();
        info.HairIndex = UnityEngine.Random.Range(0, character.Hair.Length);
        info.HeadIndex = UnityEngine.Random.Range(0, character.Head.Length);
        info.BodyIndex = UnityEngine.Random.Range(0, character.Body.Length);
        return info;
    }
}
