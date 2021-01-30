using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemPrototype : MonoBehaviour, IDropItem
{
    public Collider2D Collider;

    public SpriteRenderer SpriteRenderer;

    public Rigidbody2D Rigidbody;

    public PropertyItem Property { get; private set; }
    
    public IDropItemable AttachTo { get; set; }

    public void Bind(PropertyItem item)
    {
        Property = item;
        if (Property != null)
        {
            SpriteRenderer.sprite = item.Appearance;
        }
    }

    internal void SetTrash()
    {
        Property.GeneratedItem.WasTrash = true;
    }

    public void SetParent(Transform itemLocation)
    {
        transform.SetParent(itemLocation);
    }

    public void OnDrop()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.None;
        DropAnimation();
    }

    public void OnPick()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void OnEnterSlot()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        //Collider.enabled = false;
        DropAnimation();
    }

    public void OnLeaveSlot()
    {
        Rigidbody.constraints = RigidbodyConstraints2D.None;
        //Collider.enabled = true;
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateLocalPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    internal void Kill()
    {
        Destroy(gameObject);
    }

    private Sequence DropAnimationSequence;
    public void DropAnimation()
    {
        const float Scale = 0.8f;
        const float Time = 0.15f;
        DropAnimationSequence?.Kill();
        DropAnimationSequence = DOTween.Sequence()
            .Append(transform.DOScale(Scale, Time))
            .Append(transform.DOScale(1f, Time));
    }
}
