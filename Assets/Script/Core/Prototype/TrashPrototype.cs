using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPrototype : MonoBehaviour, IDropItemable
{
    public Collider2D Collider;

    public void Drop(ItemPrototype prototype)
    {
        throw new System.NotImplementedException();
    }
}
