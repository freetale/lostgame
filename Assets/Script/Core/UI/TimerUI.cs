using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public Transform TargetScale;

    public void SetTime(float normalizeTime)
    {
        TargetScale.localScale = new Vector3(1, normalizeTime, 1);
    }
}
