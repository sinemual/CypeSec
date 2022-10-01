using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollZone = 30;
    public float scrollSpeed = 5;

    public Vector2 cameraBoundsX;
    public Vector2 cameraBoundsY;

    public bool keyboardInput;
    public bool mouseInput;

    private Vector2 desiredPos;

    private bool followMode;
    private GameObject followObject;

    private void Start()
    {
        followMode = false;
        desiredPos = transform.position;
        desiredPos = SceneController.Instance.cueen.gameObject.transform.position;
        transform.position = SceneController.Instance.cueen.gameObject.transform.position;
    }

    private void Update()
    {
        float x = 0, y = 0;
        float speed = scrollSpeed * Time.deltaTime;

        if (mouseInput)
        {
            if (Input.mousePosition.x < scrollZone)
            {
                x -= speed;
                if (followMode)
                    followMode = false;
            }
            else if (Input.mousePosition.x > Screen.width - scrollZone)
            {
                x += speed;
                if (followMode)
                    followMode = false;
            }

            if (Input.mousePosition.y < 120 + scrollZone && Input.mousePosition.y > 120)
            {
                y -= speed;
                if (followMode)
                    followMode = false;
            }
            else if (Input.mousePosition.y > Screen.height - scrollZone)
            {
                y += speed;
                if (followMode)
                    followMode = false;
            }
        }

        if (keyboardInput)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                x += Input.GetAxis("Horizontal") * speed;
                if (followMode)
                    followMode = false;
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                y += Input.GetAxis("Vertical") * speed;
                if (followMode)
                    followMode = false;
            }
        }

        if (followMode)
        {
            Vector2 move = new Vector2(followObject.transform.position.x, followObject.transform.position.y);
            move.x = Mathf.Clamp(move.x, cameraBoundsX.x, cameraBoundsX.y);
            move.y = Mathf.Clamp(move.y, cameraBoundsY.x, cameraBoundsY.y);
            desiredPos = move;
            transform.position = desiredPos; //Vector2.Lerp(transform.position, desiredPos, Time.deltaTime * 1.2f);
        }
        else
        {
            Vector2 move = new Vector2(x, y) + desiredPos;
            move.x = Mathf.Clamp(move.x, cameraBoundsX.x, cameraBoundsX.y);
            move.y = Mathf.Clamp(move.y, cameraBoundsY.x, cameraBoundsY.y);
            desiredPos = move;
            transform.position = desiredPos;// Vector2.Lerp(transform.position, desiredPos, Time.deltaTime * 1.5f);
        }
    }

    public void MoveAtCueen()
    {
        FollowAt(SceneController.Instance.cueen.gameObject);
        //desiredPos = SceneController.Instance.cueen.gameObject.transform.position;
        //transform.position = SceneController.Instance.cueen.gameObject.transform.position;
    }

    public void FollowAt(GameObject _followObject)
    {
        followMode = true;
        followObject = _followObject;
    }
}
