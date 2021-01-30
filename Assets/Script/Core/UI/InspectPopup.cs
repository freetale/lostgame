using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectPopup : PanelBehaviour
{
    public SlotPrototype[] ItemSlot;

    [NaughtyAttributes.ReadOnly]
    public ItemPrototype BindItem;

    internal void Bind(ItemPrototype item)
    {
        BindItem = item;
    }
}
