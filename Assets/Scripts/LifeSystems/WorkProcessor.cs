using UnityEngine;
using System.Collections;

public class WorkProcessor : MonoBehaviour
{
    public Task currentTask;

    public void Work(Task _task)
    {
        currentTask = _task;
        currentTask.worker = GetComponent<Cim>();
        currentTask.inProgress = true;

        if (currentTask.curTaskType == Task.TaskType.Mining)
        {
            currentTask.worker.US.Memory.lookingForTag = currentTask.lookingForObjectTag;
            currentTask.worker.SM.SwitchToNewState(typeof(LookingForState));
            currentTask.worker.SM.OnStateOver = () =>
            {
                if (currentTask.worker.SM.IsThisState(typeof(LookingForState)))
                {
                    if (currentTask.worker.US.Memory.foundObject != null)
                        currentTask.worker.SM.SwitchToNewState(typeof(MiningState));
                    else
                        currentTask.worker.SM.SwitchToNewState(typeof(IdleState));
                }
                else if (currentTask.worker.SM.IsThisState(typeof(MiningState)))
                {
                    TasksManager.Instance.TaskComplete(currentTask);
                    currentTask.worker.SM.SwitchToNewState(typeof(IdleState));
                }
            };
        }
        //else if (currentTask.curTaskType == Task.TaskType.Building)
        //    SetState(State.Going);
        //else if (currentTask.curTaskType == Task.TaskType.Guarding)
        //    Debug.Log($"{currentTask.curTaskType}");
        //else if (currentTask.curTaskType == Task.TaskType.Patroling)
        //    SetState(State.LookingFor);
    }

    //public void WorkComplete(GameObject _resource, int _amount)
    //{
    //    _resource.transform.parent = transform;
    //    _resource.transform.localPosition = Vector2.zero;
    //    InHands = _resource;
    //    InHands.GetComponent<SpriteRenderer>().sortingLayerName = "Resources";
    //    SetState(State.GoToStorage);
    //}

    //public void WorkComplete()
    //{
    //    if (currentState == State.Building)
    //    {
    //        TasksManager.Instance.TaskComplete(currentTask);
    //        currentTask = null;
    //    }

    //    SetState(State.GoToIdle);
    //}

    //public void WorkComplete(GameObject _go)
    //{
    //    if (_go.transform.CompareTag("Relic"))
    //    {
    //        TasksManager.Instance.TaskComplete(currentTask);
    //        currentTask = null;
    //        _go.transform.parent = transform;
    //        _go.transform.localPosition = Vector2.zero;
    //        InHands = _go;
    //        InHands.GetComponent<SpriteRenderer>().sortingLayerName = "Resources";
    //        SetState(State.GoToStorage);
    //    }
    //}

    //public void StopWork()
    //{
    //    currentTask = null;
    //    SetState(State.GoToIdle);
    //    if (currentState == State.LookingFor)
    //    {
    //        LifeSystems.LookingFor.loockingFor = false;
    //        lookingFor.CurrentLookingForResource = Resource.Type.Null;
    //        lookingFor.CurrentLookingForGO = null;
    //    }

    //    if (InHands != null)
    //    {
    //        InHands.transform.parent = TileDataTool.DetectLocation(transform).transform;
    //        InHands.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
    //        TileDataTool.DetectLocation(transform).isResource = true;
    //        InHands = null;
    //    }
    //}

    //public void PauseWork()
    //{
    //    prevState = currentState;
    //    if (currentState == State.LookingFor)
    //    {
    //        lookingFor.loockingFor = false;
    //    }

    //    if (InHands != null)
    //    {
    //        InHands.transform.parent = TileDataTool.DetectLocation(transform).transform;
    //        TileDataTool.DetectLocation(transform).isResource = true;
    //        InHands = null;
    //    }
    //}

    //public void ContinueWork()
    //{
    //    Debug.Log("ContinueWork");
    //    SetState(prevState);
    //    //moving.isGoing = true;
    //    if (currentState == State.LookingFor)
    //        lookingFor.loockingFor = true;
    //}

    //private void Working()
    //{

    //}
}
