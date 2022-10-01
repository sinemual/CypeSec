using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
#if UNITY_EDITOR
    public static void SetTimeScale(float _scale)
    {
        Time.timeScale = _scale;
    }
#endif
}
