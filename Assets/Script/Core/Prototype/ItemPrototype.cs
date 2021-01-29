﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrototype : MonoBehaviour, IDropItem
{
    public Collider2D Collider;

    public SpriteRenderer SpriteRenderer;

    public Rigidbody2D Rigidbody;

    public GeneratedItem Bind;

    public void OnDrop()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.None;
    }

    public void OnPick()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void OnEnterSlot()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        Collider.enabled = false;
    }

    public void OnLeaveSlot()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.None;
        Collider.enabled = true;
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}
