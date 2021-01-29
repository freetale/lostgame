using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropItem
{
    void UpdatePosition(Vector3 position);
    void OnPick();
    void OnDrop();
}
