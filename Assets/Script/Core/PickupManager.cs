using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public int ItemLayer;
    public int Interact;

    public IDropItem pickitem;

    private void Update()
    {
        bool isDown = Input.GetKeyDown(KeyCode.Mouse0);
        bool isUp = Input.GetKeyUp(KeyCode.Mouse0);
        if (isDown)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
            var item = hit.collider.GetComponent<IDropItem>();
            if (item != null)
            {
                item.OnPick();
                pickitem = item;
            }
        }
        if (isUp && pickitem != null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, 10f);
            var item = hit.collider.GetComponent<IDropItem>();
        }
    }
}
