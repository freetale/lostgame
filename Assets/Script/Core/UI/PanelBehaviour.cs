﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBehaviour : MonoBehaviour
{
    public bool IsOpen { get; private set; }

    public float AnimationTime { get; } = 0.3f;

    public void Open()
    {
        OpenAsync().Forget();
    }

    public virtual async UniTask OpenAsync()
    {
        IsOpen = true;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        await transform.DOScale(1, AnimationTime).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        CloseAsync().Forget();
    }

    public virtual async UniTask CloseAsync()
    {
        IsOpen = false;
        await transform.DOScale(0, AnimationTime)
            .SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false));
    }
}