using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SessionRandom
{
    /// <summary>
    /// has 7 elemetn
    /// </summary>
    public TodayRandom[] Days;
}

public class TodayRandom
{
    public ItemInfo[] ItemInfo;

    public CharacterInfo[] Characters;
}

public class Randomizer
{
    public ItemList ItemList;
    public CharacterListAsset CharacterListAsset;

    public ItemInfo RandomItem()
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

    private CharacterInfo RandomCharacter()
    {
        var character = CharacterListAsset;
        CharacterInfo info = new CharacterInfo();
        info.HairIndex = UnityEngine.Random.Range(0, character.Hair.Length);
        info.HeadIndex = UnityEngine.Random.Range(0, character.Head.Length);
        info.BodyIndex = UnityEngine.Random.Range(0, character.Body.Length);
        info.Relation = character.Relaytion.PickRandom();

        return info;
    }

    private TodayRandom GetTodayRandom(int itemcount)
    {
        List<ItemInfo> item = new List<ItemInfo>();
        for (int i = 0; i < itemcount; i++)
        {
            item.Add(RandomItem());
        }
        List<CharacterInfo> characters = new List<CharacterInfo>();
        for (int i = 0; i < itemcount; i++)
        {
            var character = RandomCharacter();
            character.LookingForItem = item[i].Clone();
            characters.Add(character);
        }
        var today = new TodayRandom()
        {
            Characters = characters.ToArray(),
            ItemInfo = item.ToArray(),
        };
        return today;
    }

    public SessionRandom GetSessionRandom()
    {
        SessionRandom session = new SessionRandom() { };
        session.Days = new TodayRandom[7];
        for (int i = 0; i < 7; i++)
        {
            session.Days[i] = GetTodayRandom(7);
        }
        return session;
    }

}
