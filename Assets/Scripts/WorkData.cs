using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkData : MonoBehaviour
{
    public float workingTime = 1.0f;
    public string sound;
    public UnityAction OnWorkStart;
}
