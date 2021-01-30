using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropItemable
{
    bool IsEmpty { get; }

    void ItemEnter(ItemPrototype prototype);
    void ItemLeave(ItemPrototype prototype);
    bool CanDrop(ItemPrototype pickitem);
}

public interface IInteractable
{
    void OnClick();
}