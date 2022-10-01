using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Button deleteTaskButton;
    public Button cameraFollowButton;
    public Image portrietImage;
    public Text nameText;
    public Text taskTypeText;
    public Text itemNameText;

    public Task currentTask;

    public void Init(Task _task)
    {
        SetupsButtons();

        currentTask = _task;
        taskTypeText.text = $"{currentTask.curTaskType}";
        itemNameText.text = $"{currentTask.resourceType}";
        if (currentTask.building != null)
            itemNameText.text = $"{currentTask.building.buildingName}";
        if (currentTask.worker != null)
        {
            portrietImage.sprite = currentTask.worker.gameObject.GetComponent<SpriteRenderer>().sprite;
            nameText.text = currentTask.worker.cimName;
        }
    }

    public void RefreshTaskWorker()
    {
        if (currentTask.worker != null)
        {
            portrietImage.sprite = currentTask.worker.gameObject.GetComponent<SpriteRenderer>().sprite;
            nameText.text = currentTask.worker.cimName;
        }
    }

    private void SetupsButtons()
    {
        deleteTaskButton.onClick.RemoveAllListeners();
        cameraFollowButton.onClick.RemoveAllListeners();

        deleteTaskButton.onClick.AddListener(() => { TasksManager.Instance.DeleteTask(currentTask); });
        cameraFollowButton.onClick.AddListener(() => { Camera.main.GetComponent<CameraController>().FollowAt(currentTask.worker.gameObject); });
    }
}
