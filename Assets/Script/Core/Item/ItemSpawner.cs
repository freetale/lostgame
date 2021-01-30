using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [Header("Spawn")]
    public Transform SpawnLocation;
    public float SpawnSpaceX;

    public ItemPool ItemPool;

    public void Spawn(ItemInfo[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            var prototype = ItemPool.PickOne();
            prototype.Bind(items[i]);
            var position = SpawnLocation.position;
            position.x += i * SpawnSpaceX;
            prototype.UpdatePosition(position);
        }
    }
}
