using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCall : MonoBehaviour, IInteractable
{
    public Action OnInteract;

    public void OnClick()
    {
        OnInteract();
    }
}
