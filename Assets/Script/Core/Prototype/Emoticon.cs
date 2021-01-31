using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoticon : MonoBehaviour
{
    public Transform Transform;

    public GameObject Showing;

    private Sequence Sequence;
    public float MoveY = 1.5f;
    public float MoveDuration = 1.7f;
    public void Show()
    {
        Sequence?.Kill();
        Showing.SetActive(true);
        Sequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveY(MoveY, MoveDuration))
            .OnComplete(() => Showing.SetActive(false))
            .Play();
    }
}
