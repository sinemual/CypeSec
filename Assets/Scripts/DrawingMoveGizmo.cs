using UnityEngine;
using System.Collections;

public class DrawingMoveGizmo : Moving
{
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
