using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPrototype : MonoBehaviour, IDropItemable
{
    public Transform ItemLocation;
    public Collider2D Collider;

    public ItemPrototype BindItem { get; private set; }
    public bool IsEmpty => BindItem == null;

    public bool IsTrash;

    public System.Predicate<ItemPrototype> PreDropCheck;

    public bool CanDrop(ItemPrototype prototype)
    {
        if (PreDropCheck != null)
        {
            return PreDropCheck(prototype);
        }
        return true;
    }
    public void ItemEnter(ItemPrototype prototype)
    {
        BindItem = prototype;
        prototype.SetParent(ItemLocation);
        prototype.UpdateLocalPosition(Vector3.zero);
        if (IsTrash)
        {
            prototype.SetTrash();
        }
    }

    public void ItemLeave(ItemPrototype prototype)
    {
        BindItem = null;
        prototype.SetParent(null);
    }

}
