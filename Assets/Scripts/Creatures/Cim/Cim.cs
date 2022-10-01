using UnityEngine;
using UnityEngine.Events;

public class Cim : Unit
{
    public string cimName;
    public WorkProcessor workProcessor;

    private void Start()
    {
        cimName = SceneController.Instance.GetComponent<UnitsNames>().GetName();
        workProcessor = gameObject.AddComponent<WorkProcessor>();
    }

    //public void SetState(State _state, GoTo _goTo = GoTo.Null)
    //{
    //    currentState = _state;
    //    if (currentState == State.LookingFor)
    //    {
    //        if (currentTask.curTaskType == Task.TaskType.Mining)
    //            US.LookingFor.CurrentLookingForResource = currentTask.resourceType;
    //        if (currentTask.curTaskType == Task.TaskType.Patroling)
    //            US.LookingFor.CurrentLookingForGO = SceneController.Instance.patrolPoint;
    //        US.LookingFor.loockingFor = true;
    //    }
    //    else if (currentState == State.Going)
    //    {
    //        UnityAction completeCallBack = () => { SetState(State.IsIdle); };
    //        US.Moving.GoTo(StorageManager.Instance.idlePoint, completeCallBack);
    //    }
    //    else if (currentState == State.IsIdle)
    //    {
    //        SceneController.Instance.cueen.AskTask(this);
    //    }
    //    else if (currentState == State.GoToStorage)
    //    {
    //        if (ObjectInHand != null && ObjectInHand.CompareTag("Relic"))
    //        {
    //            UnityAction completeCallBack = () =>
    //            {
    //                StorageManager.Instance.PutRelic(inHands.GetComponent<Relic>());
    //                inHands = null;
    //                SetState(State.GoToIdle);
    //            };
    //            moving.GoTo(loadUnloadPlace, completeCallBack);
    //        }
    //        else if (currentTask.curTaskType == Task.TaskType.Building)
    //        {
    //            UnityAction completeCallBack = () =>
    //            {
    //                if (StorageManager.Instance.IsResourceAvaliable(currentTask.building.NeedResource().Item1, currentTask.building.NeedResource().Item2))
    //                {
    //                    GameObject _temp = Instantiate(StorageManager.Instance.GetResource(currentTask.building.NeedResource().Item1, currentTask.building.NeedResource().Item2));
    //                    _temp.GetComponent<Resource>().currrentResType = currentTask.building.NeedResource().Item1;
    //                    _temp.GetComponent<Resource>().amount = currentTask.building.NeedResource().Item2;
    //                    _temp.transform.parent = transform;
    //                    _temp.transform.localPosition = Vector2.zero;
    //                    inHands = _temp;
    //                    inHands.GetComponent<SpriteRenderer>().sortingLayerName = "Resources";
    //                    SetState(State.GoToBuilding);
    //                }
    //                else
    //                {
    //                    SetState(State.GoToIdle);
    //                }
    //            };
    //            moving.GoTo(loadUnloadPlace, completeCallBack);
    //        }

    //    else if (currentState == State.GoToBuilding)
    //    {
    //        UnityAction completeCallBack = () =>
    //        {
    //            currentTask.building.AddResource(inHands.GetComponent<Resource>().currrentResType, inHands.GetComponent<Resource>().amount);
    //            Destroy(inHands);
    //            if (currentTask.building.IsHaveNeedResources())
    //            {
    //                currentTask.building.BuildThis(this);
    //                SetState(State.Building);
    //            }
    //            else
    //                SetState(State.GoToStorage);
    //        };
    //        moving.GoTo(TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(currentTask.building.transform), TileDataTool.DetectLocation(transform)), completeCallBack);
    //    }

    //    else if (currentState == State.Die)
    //    {
    //        lookingFor.loockingFor = false;
    //        moving.isGoing = false;
    //        if (currentTask != null)
    //            TasksManager.Instance.DeleteTask(currentTask);
    //    }
    //    anim.SetState(currentState);
    //}

    //public void Communicate(Cim _cim)
    //{
    //    //if (_cim.currentTask.curTaskType == Task.TaskType.Mining)
    //    //{
    //    //    //if(_cim.currentTask.resourceType == Resource.Type.Meat && memory.meatyPoint != null)
    //    //}
    //}

    public override void Die()
    {
        IsDead = true;
        //SetState(State.Dead);
    }

    public override void ModifyHealth(int _amount)
    {
        Health += _amount;
        if (Health <= 0)
            Die();
    }

    //public void AttackTarget(GameObject _go)
    //{
    //    PauseWork();
    //    fightingWith = _go;
    //    lookingFor.loockingFor = false;
    //    //moving.isGoing = false;
    //    targerPos = TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(fightingWith.transform), TileDataTool.DetectLocation(transform));
    //    Attack();
    //}

    //private void Attack()
    //{
    //    Debug.Log(TileDataTool.Heuristic(TileDataTool.DetectLocation(transform), TileDataTool.DetectLocation(fightingWith.transform)));
    //    if (TileDataTool.Heuristic(TileDataTool.DetectLocation(transform), targerPos) <= 1)
    //    {
    //        SetState(State.Fighting); //AttackAnim
    //    }
    //    else
    //    {
    //        UnityAction completeCallback = () =>
    //        {
    //            Attack();
    //        };
    //        SetState(State.Going);
    //        if (fightingWith == null)
    //        {
    //            ContinueWork();
    //            return;
    //        }
    //        targerPos = TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(fightingWith.transform), TileDataTool.DetectLocation(transform));
    //        moving.GoTo(targerPos, completeCallback);
    //    }
    //}

    //public void AttackAnimOver()
    //{
    //    fightingWith.GetComponent<Stats>().ToDamage(stats.Damage);
    //    if (fightingWith.GetComponent<Stats>().isDead)
    //    {
    //        fightingWith = null;
    //        ContinueWork();
    //        return;
    //    }
    //    else
    //        CoroutineActions.ExecuteAction(attackReloadTime, () => { Attack(); });

    //    SetState(State.IsIdle);
    //}
}