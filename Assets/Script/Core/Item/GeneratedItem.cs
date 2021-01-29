using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GeneratedItem
{
    public string Name;
    public Dictionary<string, string> Property = new Dictionary<string, string>();
}

public class PropertyItem
{
    public GeneratedItem GeneratedItem;

    public GameObject Prototype;
    public Sprite Appearance;
}