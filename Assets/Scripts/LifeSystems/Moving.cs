using System;
using UnityEngine;
using UnityEngine.Events;

public class Moving : MonoBehaviour
{
    public TileData destinationPoint { get; set; }
    public UnityAction OnHasCome { get; set; }

    protected Transform[] currentPath;
    private int pointCounter;
    private bool isGoing;

    public void GoTo(TileData _destinationPoint, UnityAction _onHasCome = null)
    {
        destinationPoint = _destinationPoint;
        OnHasCome = _onHasCome;
        currentPath = AStar.GetPath(TileDataTool.DetectLocation(transform), destinationPoint);
        pointCounter = 0;
        if (currentPath.Length > 0)
            isGoing = true;
        else
            Debug.LogError("Path not getting");
    }

    private void Update()
    {
        if (isGoing)
            Going();
    }

    public void Going()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPath[pointCounter].position, /*unit.Speed **/ Time.deltaTime);

        if (Vector2.Distance(transform.position, currentPath[pointCounter].position) < Mathf.Epsilon)
        {
            if (pointCounter == currentPath.Length - 1)
            {
                isGoing = false;
                OnHasCome?.Invoke();
                return;
            }
            pointCounter++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            if (currentPath != null)
            {
                Transform itemPrev = currentPath[0];
                foreach (Transform item in currentPath)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawSphere(item.position, 0.1f);
                    Gizmos.DrawLine(item.position, itemPrev.position);
                    itemPrev = item;
                }
            }
        }
    }
}

//public interface IMoveble
//{
//    TileData DestinationPoint { get; set; }
//    UnityAction OnHasCome { get; set; }
//}
