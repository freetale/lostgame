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
    public string[] Name;
    public string[] LastName;
}

[Serializable]
public class CharacterInfo
{
    public int HeadIndex;
    public int BodyIndex;
    public int HairIndex;

    public bool IsRelaytive;
    public bool IsImposter;

    public string Relation;
    public ItemInfo LookingForItem;

    public Guid guid;
    public string Name;
    public int Satisfaction = 10;
}