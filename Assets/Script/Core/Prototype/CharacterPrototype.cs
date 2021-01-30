using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{
    None,
    Happy,
    Angry,
}


public class CharacterPrototype : MonoBehaviour
{
    public SpriteRenderer HeadRenderer;
    public SpriteRenderer BodyRenderer;
    public SpriteRenderer HairRenderer;

    public GameObject AngryIcon;
    public GameObject HappyIcon;

    public CharacterListAsset CharacterListAsset;

    public void Bind(CharacterInfo characterInfo)
    {
        try
        {
            HairRenderer.sprite = CharacterListAsset.Hair[characterInfo.HairIndex];
            HeadRenderer.sprite = CharacterListAsset.Head[characterInfo.HeadIndex];
            BodyRenderer.sprite = CharacterListAsset.Body[characterInfo.BodyIndex];
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            throw;
        }
    }

    public void SetEmotion(Emotion emotion)
    {
        AngryIcon.SetActive(emotion == Emotion.Angry);
        HappyIcon.SetActive(emotion == Emotion.Happy);
    }

    [NaughtyAttributes.Button]
    public void RandomCharacter()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        Randomizer randomizer = new Randomizer();
        var info = randomizer.RandomCharacter(CharacterListAsset);
        Bind(info);
    }
}
