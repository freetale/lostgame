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

    public Emoticon AngryIcon;
    public Emoticon HappyIcon;

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
        if (emotion == Emotion.Angry)
            AngryIcon.Show();
        if (emotion == Emotion.Happy)
            HappyIcon.Show();
    }

}
