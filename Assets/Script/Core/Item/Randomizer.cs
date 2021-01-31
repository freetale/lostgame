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
        generated.OwnerName = RandomName();

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
        string propertyAppearance;
        if (!generated.Property.TryGetValue(item.AppearancesKey, out propertyAppearance))
        {
            throw new Exception();
        }
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

    private TodayRandom GetTodayRandom(int date, int itemcount, int relaytive, int imposter)
    {
        List<ItemInfo> items = new List<ItemInfo>();
        for (int i = 0; i < itemcount; i++)
        {
            ItemInfo item = RandomItem();
            item.Date = date;
            items.Add(item);
        }
        List<CharacterInfo> characters = new List<CharacterInfo>();
        for (int i = 0; i < itemcount; i++)
        {
            var character = RandomCharacter();
            ItemInfo itemInfo = items[i].Clone();
            if (i < relaytive)
            {
                if (UnityEngine.Random.value < 0.5)
                {
                    itemInfo.Location = null;
                }
                else
                {
                    itemInfo.Room = null;
                }
            }
            character.LookingForItem = itemInfo;
            characters.Add(character);
        }
        for (int i = 0; i < imposter; i++)
        {
            var character = RandomCharacter();
            var item = items.PickRandom().Clone();
            if (UnityEngine.Random.value < 0.5)
            {
                item.Location = null;
            }
            else
            {
                item.Room = null;
            }
            character.IsImposter = true;
            character.LookingForItem = item;
            characters.Add(character);
        }
        characters.Shuffle();
        var today = new TodayRandom()
        {
            Characters = characters.ToArray(),
            ItemInfo = items.ToArray(),
        };
        return today;
    }

    private string RandomName()
    {
        return string.Format(CharacterListAsset.Name.PickRandom(), CharacterListAsset.LastName.PickRandom());
    }

    public SessionRandom GetSessionRandom()
    {
        SessionRandom session = new SessionRandom() { };
        session.Days = new TodayRandom[7];
        session.Days[0] = GetTodayRandom(0, 3, 1, 1);
        session.Days[1] = GetTodayRandom(1, 3, 0, 0);
        session.Days[2] = GetTodayRandom(2, 3, 1, 0);
        session.Days[3] = GetTodayRandom(3, 3, 1, 1);
        session.Days[4] = GetTodayRandom(4, 3, 1, 1);
        session.Days[5] = GetTodayRandom(5, 3, 2, 2);
        session.Days[6] = GetTodayRandom(6, 3, 2, 2);
        return session;
    }

}
