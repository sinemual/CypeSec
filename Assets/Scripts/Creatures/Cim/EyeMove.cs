using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMove : MonoBehaviour
{
    public Vector2 boredersX;
    public Vector2 boredersY;

    private Moving moving;
    private Vector2 desiredPos;
    private void Start()
    {
        moving = GetComponentInParent<Moving>();
    }

    private void FixedUpdate()
    {
        //if(moving.currentPath[moving.pointCounter] != null)
        //{
        //    float x = 0, y = 0;
        //    desiredPos = moving.currentPath[moving.pointCounter].position;
        //    Vector2 move = new Vector2(x, y) + desiredPos;
        //    move.x = Mathf.Clamp(move.x, boredersX.x, boredersX.y);
        //    move.y = Mathf.Clamp(move.y, boredersY.x, boredersY.y);
        //    desiredPos = move;
        //    transform.localPosition = Vector2.Lerp(transform.localPosition, desiredPos, Time.deltaTime);
        //}
    }
}
