using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private List<ItemPrototype> Objects = new List<ItemPrototype>();

    public GameObject Prototype;

    public ItemPrototype PickOne()
    {
        var item = Objects.Find(i => !i.gameObject.activeSelf);
        if (item == null)
        {
            item = InstanceOne();
        }
        return item;
    }

    private ItemPrototype InstanceOne()
    {
        var item = Instantiate(Prototype).GetComponent<ItemPrototype>();
        Objects.Add(item);
        return item;
    }
}
