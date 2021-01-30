using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropItemable
{
    bool IsEmpty { get; }

    void ItemEnter(ItemPrototype prototype);
    void ItemLeave(ItemPrototype prototype);
}
public interface IDropItem
{
    void UpdatePosition(Vector3 position);
    void OnPick();
    void OnDrop();
}

public interface IInteractable
{
    void OnClick();
}