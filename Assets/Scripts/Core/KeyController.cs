using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            UIManager.Instance.BuildingsPanelToggle();
        if (Input.GetKeyDown(KeyCode.M))
            UIManager.Instance.MiningPanelToggle();
        if (Input.GetKeyDown(KeyCode.T))
            UIManager.Instance.TaskPanelToggle();
        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.Instance.PausePanelToggle();

        if (Input.GetKeyDown(KeyCode.Q))
            Camera.main.GetComponent<CameraController>().MoveAtCueen();
        if (Input.GetKeyDown(KeyCode.E))
            SceneController.Instance.cueen.ceggsManager.CreateCegg();
        if (Input.GetKeyDown(KeyCode.Space))
            SceneController.Instance.Alarm();
        if (SceneController.Instance.currentGameState == SceneController.GameState.Building && Input.GetKeyDown(KeyCode.R))
            BuildingsManager.Instance.RotatePick();

        CameraFollow();
    }

    private void CameraFollow()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[0].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[1].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[2].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[3].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[4].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[5].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[6].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[7].gameObject);
        if (Input.GetKeyDown(KeyCode.Alpha9))
            Camera.main.GetComponent<CameraController>().FollowAt(SceneController.Instance.cueen.cims[8].gameObject);
    }
}
