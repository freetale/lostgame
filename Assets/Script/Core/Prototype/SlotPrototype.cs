using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPrototype : MonoBehaviour, IDropItemable
{
    public Transform ItemLocation;
    public Collider2D Collider;

    public ItemPrototype BindItem { get; private set; }

    public void Drop(ItemPrototype prototype)
    {
        prototype.OnEnterSlot();
    }
}
