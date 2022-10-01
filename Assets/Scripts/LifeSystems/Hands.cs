using System;
using UnityEngine;
using UnityEngine.Events;

public class Hands : MonoBehaviour, IHaveHands
{
    public GameObject InHands { get; set; }
    public UnityAction OnTake { get; set; }
    public UnityAction OnDrop { get; set; }

    protected Transform[] currentPath;
    private int pointCounter;
    private bool isGoing;

    public void Take(GameObject _object)
    {
        InHands = _object;
        _object.transform.parent = transform;
        _object.transform.localPosition = Vector3.zero;
        OnTake?.Invoke();
    }

    public void Drop()
    {
        InHands.transform.parent = null;
        InHands = null;
        OnDrop?.Invoke();
    }
}

public interface IHaveHands
{
    GameObject InHands { get; set; }
}
