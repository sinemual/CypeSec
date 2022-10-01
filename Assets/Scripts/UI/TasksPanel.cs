using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksPanel : MonoBehaviour
{
    public GameObject tasksParent;
    private List<TaskUI> tasksUI = new List<TaskUI>();

    private void Start()
    {
        tasksUI.AddRange(tasksParent.GetComponentsInChildren<TaskUI>());
        foreach (var item in tasksUI)
            item.gameObject.SetActive(false);
    }

    public void AddTask(Task _task)
    {
        for (int i = 0; i < tasksUI.Count; i++)
        {
            if (!tasksUI[i].gameObject.activeSelf)
            {
                tasksUI[i].gameObject.SetActive(true);
                tasksUI[i].Init(_task);
                return;
            }
        }
    }

    public void RefreshInfo(Task _task)
    {
        for (int i = 0; i < tasksUI.Count; i++)
        {
            if (tasksUI[i].currentTask == _task)
            {
                tasksUI[i].RefreshTaskWorker();
                return;
            }
        }
    }

    public void DeleteTask(Task _task)
    {
        for (int i = 0; i < tasksUI.Count; i++)
            if (tasksUI[i].currentTask == _task)
                tasksUI[i].gameObject.SetActive(false);
    }
}
