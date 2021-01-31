using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkPopup : PanelBehaviour
{
    public TMP_Text Text;

    public float CloseDelay = 3f;

    private float CloseTime;
    
    private bool cancle;

    private Coroutine Coroutine;

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void Cancle()
    {
        if (Coroutine != null)
        {
            StopCoroutine(Coroutine);
            Coroutine = null;
        }
    }

    public void WaitAndClose()
    {
        CloseTime = CloseDelay + Time.time;
        if (Coroutine == null)
        {
            Open();
            Coroutine = StartCoroutine(WaitAndCloseRoutine());
        }
    }

    private IEnumerator WaitAndCloseRoutine()
    {
        yield return new WaitUntil (() => CloseTime < Time.time);
        Close();
        Coroutine = null;
    }
}
