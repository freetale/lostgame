using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectPopup : PanelBehaviour
{
    public SlotPrototype[] ItemSlot;

    [NaughtyAttributes.ReadOnly]
    public ItemPrototype BindItem;

    [NaughtyAttributes.ReadOnly]
    public ItemPool ItemPool;
    [NaughtyAttributes.ReadOnly]
    public Randomizer Randomizer;

    private void Awake()
    {
        foreach (var item in ItemSlot)
        {
            item.PreDropCheck = PreCheck;
        }
    }

    private void Start()
    {
        ItemPool = GameplayManager.Instance.ItemPool;
        Randomizer = GameplayManager.Instance.Randomizer;
    }

    private bool PreCheck(ItemPrototype obj)
    {
        if (obj == BindItem)
        {
            return false;
        }
        return true;
    }

    internal void Bind(ItemPrototype item)
    {
        foreach (var slot in ItemSlot)
        {
            if (slot.BindItem)
            {
                ItemPool.Return(slot.BindItem);
            }
        }
        BindItem = item;
        if (item != null)
        {
            var generate = item.Property.GeneratedItem;
            foreach (var subItem in generate.ItemInfos)
            {
                var property = Randomizer.MatchProperty(subItem);
                var prototype = ItemPool.PickOne();
                prototype.Bind(property);
            }
        }
    }
}
