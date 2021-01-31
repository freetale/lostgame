using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashPrototype : MonoBehaviour, IDropItemable, IInteractable
{
    public Collider2D Collider;

    [Required]
    public TrashPopup TrashPopup;
    public SlotPrototype[] TrashSlot => TrashPopup.TrashSlot;

    public AudioClip TrashSound;

    public bool IsEmpty { get; } = true;

    public bool CanDrop(ItemPrototype pickitem)
    {
        return true;
    }

    public void ItemEnter(ItemPrototype prototype)
    {
        var slot = TrashSlot.FirstOrDefault(i => i.BindItem == null);
        if (slot == null)
        {
            for (int i = TrashSlot.Length - 1; i >= 0; i--)
            {
                SlotPrototype current = TrashSlot[i];
                SlotPrototype next = default;
                if (i + 1 < TrashSlot.Length)
                {
                    next = TrashSlot[i + 1];
                }
                var item = current.BindItem;
                if (item != null)
                {
                    item.AttachTo = null;
                    item.OnLeaveSlot();
                    current.ItemLeave(item);
                }
                if (next != null && item != null)
                {
                    item.AttachTo = next;
                    next.ItemEnter(item);
                    item.OnEnterSlot();
                }
                if (next == null && item != null)
                {
                    item.Kill();
                }
            }
            slot = TrashSlot[0];
        }
        prototype.AttachTo = slot;
        slot.ItemEnter(prototype);
        prototype.OnEnterSlot();
        GameplayManager.Instance.PlayerSfx(TrashSound);
    }

    public void ItemLeave(ItemPrototype prototype)
    {
        var slot = TrashSlot.FirstOrDefault(i => i.BindItem == prototype);
        if (!slot)
        {
            Debug.LogError("slot");
        }
    }

    public void OnClick()
    {
        if (TrashPopup.IsOpen)
        {
            TrashPopup.Close();
        }
        else
        {
            TrashPopup.Open();
        }
    }
}
