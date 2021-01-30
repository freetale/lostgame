using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterInfoAsset : ScriptableObject
{
    public Sprite[] Head;
    public Sprite[] Body;
    public Sprite[] Hair;
}

[Serializable]
public class CharacterInfo
{
    public int HeadIndex;
    public int BodyIndex;
    public int HairIndex;

    public bool IsRelaytive;
    public bool IsImposter;

    public GeneratedItem LookingForItem;
}