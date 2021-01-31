using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnClick : MonoBehaviour, IPointerClickHandler
{
    public AudioClip AudioClip;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSource.PlayClipAtPoint(AudioClip, Vector3.zero);
    }
}
