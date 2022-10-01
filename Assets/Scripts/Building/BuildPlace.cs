using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlace : MonoBehaviour
{
    private int layerMask = ~(1 << 9); // slots on layer 9

    public void Check()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, layerMask);
        if (hit.collider != null && hit.collider.gameObject != gameObject)
            gameObject.SetActive(false);
    }
}
