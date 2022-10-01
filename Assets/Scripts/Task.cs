using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public enum TaskType { Guarding, Mining, Building, Wander };
    public TaskType curTaskType;
    public TileData taskPos;
    public Building building;
    public Resource.Type resourceType;
    public string lookingForObjectTag;
    public bool inProgress;
    public Cim worker;

    public void TaskInit(TaskType _taskType, TileData _pos)
    {
        curTaskType = _taskType;
        taskPos = _pos;
        Debug.Log($"New task! Type:{curTaskType}");
    }

    public void TaskInit(TaskType _taskType, Building _building)
    {
        curTaskType = _taskType;
        building = _building;
        Debug.Log($"New task! Type:{curTaskType}");
    }

    public void TaskInit(TaskType _taskType, Resource.Type _resourceType)
    {
        curTaskType = _taskType;
        resourceType = _resourceType;
        lookingForObjectTag = _resourceType.ToString();
        Debug.Log($"New task! Type:{curTaskType}, search:{resourceType}");
    }

    public void TaskInit(TaskType _taskType)
    {
        curTaskType = _taskType;
        Debug.Log($"New task! Type:{curTaskType}");
    }
}
