using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CharacterListAsset : ScriptableObject
{
    public Sprite[] Head;
    public Sprite[] Body;
    public Sprite[] Hair;

    public string[] Relaytion;
}

[Serializable]
public class CharacterInfo
{
    public int HeadIndex;
    public int BodyIndex;
    public int HairIndex;

    public bool IsRelaytive;
    public bool IsImposter;

    public string Relaytion;
    public ItemInfo LookingForItem;
}