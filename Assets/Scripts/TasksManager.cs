using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{

    #region Singleton Init
    private static TasksManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static TasksManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<TasksManager>();
        _instance.Initialize();
    }
    #endregion  

    public List<Task> tasks;
    public TasksPanel tasksPanel;
    public GameObject taskParent;
    public GameObject p_task;

    private void Initialize()
    {

    }

    public void AddTask(Task.TaskType _taskType, TileData _pos)
    {
        GameObject _taskGO = Instantiate(p_task, taskParent.transform);
        Task _task = _taskGO.GetComponent<Task>();
        _task.TaskInit(_taskType, _pos);
        tasks.Add(_task);
        SceneController.Instance.cueen.NewTask(_task);
    }

    public void AddTask(Task.TaskType _taskType, Resource.Type _resourceType)
    {
        GameObject _taskGO = Instantiate(p_task, taskParent.transform);
        Task _task = _taskGO.GetComponent<Task>();
        _task.TaskInit(_taskType, _resourceType);
        tasks.Add(_task);
        SceneController.Instance.cueen.NewTask(_task);
    }

    public void AddTask(Task.TaskType _taskType, Building _building)
    {
        GameObject _taskGO = Instantiate(p_task, taskParent.transform);
        Task _task = _taskGO.GetComponent<Task>();
        _task.TaskInit(_taskType, _building);
        tasks.Add(_task);
        SceneController.Instance.cueen.NewTask(_task);
    }

    public void AddTask(Task.TaskType _taskType)
    {
        GameObject _taskGO = Instantiate(p_task, taskParent.transform);
        Task _task = _taskGO.GetComponent<Task>();
        _task.TaskInit(_taskType);
        tasks.Add(_task);
        SceneController.Instance.cueen.NewTask(_task);
    }

    public void TaskComplete(Task _task)
    {
        tasksPanel.DeleteTask(_task);
        Destroy(_task.gameObject);
        tasks.Remove(_task);
    }

    public void DeleteTask(Task _task)
    {
        tasksPanel.DeleteTask(_task);
        Destroy(_task.gameObject);
        tasks.Remove(_task);
    }
}