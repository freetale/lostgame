using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimation
{
    None,
    MoveIn,
    MoveOut,
    PoliceMoveIn,
    PoliceMoveout,
}

public class CharacterControlGroup : MonoBehaviour
{
    [Required]
    public Animator Animator;
    public string NoneAnimation;
    public string MoveInAnimation;
    public string MoveOutAnimation;
    public string PoliceMoveInAnimation;
    public string PoliceMoveOutAnimation;

    public CharacterPrototype CharacterPrototype;

    public async UniTask Play(CharacterAnimation animation)
    {
        var name = GetAnimationName(animation);
        Animator.Play(name);
        var state = Animator.GetCurrentAnimatorStateInfo(0);
        await UniTask.Delay((int)(state.length * 1000));
    }

    private string GetAnimationName(CharacterAnimation animation)
    {
        switch (animation)
        {
            case CharacterAnimation.None: return NoneAnimation;
            case CharacterAnimation.MoveIn: return MoveInAnimation;
            case CharacterAnimation.MoveOut: return MoveOutAnimation;
            case CharacterAnimation.PoliceMoveIn: return PoliceMoveInAnimation;
            case CharacterAnimation.PoliceMoveout: return PoliceMoveOutAnimation;
            default:
                Debug.Log("Warning animation missing" + animation);
                return NoneAnimation;
        }
    }

    internal void Bind(CharacterInfo characterInfo)
    {
        CharacterPrototype.Bind(characterInfo);
    }
}
