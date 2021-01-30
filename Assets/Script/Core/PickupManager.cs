using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupManager : MonoBehaviour
{
    public LayerMask ItemLayer;
    public LayerMask Interact;

    [ReadOnly]
    public ItemPrototype pickitem;

    public Camera Camera;

    public float ClickTrashold = 0.3f;

    private float keyDownTime;

    private void Start()
    {
        if (!Camera)
            Camera = Camera.main;
    }

    private void Update()
    {
        bool isDown = Input.GetKeyDown(KeyCode.Mouse0);
        bool isUp = Input.GetKeyUp(KeyCode.Mouse0);
        if (isDown)
        {
            keyDownTime = Time.unscaledTime;
            PickItem();
        }
        if (pickitem)
        {
            var position = Input.mousePosition;
            var world = Camera.ScreenToWorldPoint(position);
            world.z = 0;
            pickitem.UpdatePosition(world);
        }
        if (isUp)
        {
            if (keyDownTime + ClickTrashold > Time.unscaledTime)
            {
                if (pickitem != null)
                {
                    GameplayManager.Instance.InspectItem(pickitem);
                    DropItem();
                }
                else
                {
                    Interacting();
                }
            }
            else if (pickitem != null)
            {
                DropItem();
            }
        }
    }

    private void PickItem()
    {
        var ray = Camera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, ItemLayer, 0);
        if (!hit.collider)
        {
            return;
        }
        var item = hit.collider.GetComponent<ItemPrototype>();
        if (item != null)
        {
            if (item.AttachTo != null)
            {
                item.AttachTo.ItemLeave(item);
                item.OnLeaveSlot();
                item.AttachTo = null;
            }
            item.OnPick();
            pickitem = item;
        }
    }

    private void DropItem()
    {
        var ray = Camera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, Interact, 0);
        if (!hit.collider)
        {
            DropToFloor();
            return;
        }
        var reciver = hit.collider.GetComponent<IDropItemable>();
        if (reciver == null)
        {
            DropToFloor();
            return;
        }
        else
        {
            if (!reciver.IsEmpty)
            {
                DropToFloor();
            }
            else
            {
                pickitem.AttachTo = reciver;
                pickitem.OnDrop();
                pickitem.OnEnterSlot();
                reciver.ItemEnter(pickitem);
                pickitem = null;
            }
        }
    }

    private void DropToFloor()
    {
        pickitem.OnDrop();
        pickitem = null;
    }

    private void Interacting()
    {
        var ray = Camera.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, Interact, 0);
        if (!hit.collider)
        {
            return;
        }
        var reciver = hit.collider.GetComponent<IInteractable>();
        if (reciver != null)
        {
            reciver.OnClick();
        }
    }
}
