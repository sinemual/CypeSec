using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stranger : MonoBehaviour
{
    public enum StrangerType { Knight, Miner, Thief, Wizard }
    public StrangerType currentStrangerType;
    public enum Goals { KillMonsters, FindTreasures, StealRelics, LookingFor, MineMetaly }
    public Goals currentGoal;
    public enum State { IsIdle, Fighting, Going, Leaving, LookingFor, Minig, Die }
    public State currentState;
    public State prevState;
    public bool goalIsComplete;
    public GameObject fightingWith;
    public float attackReloadTime;
    //public bool isMayAttack;
    public GameObject inHands;
    public GameObject p_meat;

    private TileData goalPos;
    private string strangerName;
    private TileData targerPos;

    //Systems
    [HideInInspector] public Memory memory;
    [HideInInspector] public Vision vision;
    [HideInInspector] public Moving moving;
    [HideInInspector] public Stats stats;
    [HideInInspector] public AnimatorController anim;

    //private void Start()
    //{
    //    memory = GetComponent<Memory>();
    //    vision = GetComponent<Vision>();
    //    moving = GetComponent<Moving>();
    //    stats = GetComponent<Stats>();
    //    anim = GetComponent<AnimatorController>();

    //    strangerName = SceneController.Instance.GetComponent<UnitsNames>().GetName();
    //    SetState(State.IsIdle);
    //    Init();
    //}

    //private void Init()
    //{
    //    //int lvl = Data.Instance.level;
    //    //stats.HP = 10 * lvl;
    //    //stats.agility = Random.Range(1, lvl);
    //    //stats.strong = Random.Range(1, lvl);
    //    //stats.intellect = Random.Range(1, lvl);
    //    stats.onDie += Die;

    //    if (currentStrangerType == StrangerType.Knight)
    //        SetGoal(Goals.KillMonsters);
    //    if (currentStrangerType == StrangerType.Miner)
    //        SetGoal(Goals.MineMetaly);
    //    if (currentStrangerType == StrangerType.Thief)
    //        SetGoal(Goals.StealRelics);
    //}

    //private void SetGoal(Goals _goal)
    //{
    //    currentGoal = _goal;

    //    if (currentGoal == Goals.KillMonsters)
    //        SetState(State.LookingFor);
    //    if (currentGoal == Goals.MineMetaly)
    //        SetState(State.LookingFor);
    //    if (currentGoal == Goals.StealRelics)
    //        SetState(State.LookingFor);
    //}

    //public void DoWithFound(TileData _tileData)
    //{
    //    if (currentGoal == Goals.MineMetaly)
    //    {
    //        SetState(State.Minig);
    //        _tileData.GetComponentInChildren<Resource>().Mine(this);
    //    }
    //}

    //public void DoWithFound(GameObject _go)
    //{
    //    if (_go == SceneController.Instance.cueen.gameObject)
    //    {
    //        fightingWith = _go;
    //        if (fightingWith == SceneController.Instance.cueen.gameObject)
    //            targerPos = TileDataTool.DetectLocation(fightingWith.transform).neighbors[(int)TileData.Dirs.W];
    //        else
    //            targerPos = TileDataTool.DetectLocation(fightingWith.transform);
    //        Attack();
    //    }

    //    if (_go.CompareTag("Relic"))
    //    {
    //        lookingFor.loockingFor = false;
    //        UnityAction completeCallBack = () =>
    //        {
    //            _go.transform.parent = transform;
    //            _go.transform.localPosition = Vector2.zero;
    //            inHands = _go;
    //            inHands.GetComponent<SpriteRenderer>().sortingLayerName = "Resources";
    //            SetState(State.Leaving);
    //        };
    //        moving.GoTo(TileDataTool.DetectLocation(_go.transform), completeCallBack);
    //    }
    //}

    public void WorkComplete(GameObject _resource, int _amount)
    {
        _resource.transform.parent = transform;
        _resource.transform.localPosition = Vector2.zero;
        inHands = _resource;
        inHands.GetComponent<SpriteRenderer>().sortingLayerName = "Resources";
        //SetState(State.Leaving);
    }

    //public void SetState(State _state)
    //{
    //    currentState = _state;
    //    if (currentState == State.LookingFor)
    //    {
    //        //lookingFor.loockingFor = false;
    //        if (currentGoal == Goals.KillMonsters)
    //            lookingFor.CurrentLookingForGO = SceneController.Instance.cueen.gameObject;
    //        else if (currentGoal == Goals.MineMetaly)
    //            lookingFor.CurrentLookingForResource = Resource.Type.Metaly;
    //        else if (currentGoal == Goals.StealRelics)
    //            lookingFor.CurrentLookingForGO = StorageManager.Instance.relics[Random.Range(0, StorageManager.Instance.relics.Count)].gameObject;
    //    }
    //    if (currentState == State.Fighting)
    //    {

    //    }
    //    if (currentState == State.Leaving)
    //    {
    //        UnityAction completeCallback = () =>
    //        {
    //            //for (int i = 0; i < SceneController.Instance.cueen.cims.Count; i++)
    //            // if(SceneController.Instance.cueen.cims[i].fightingWith == gameObject)
    //            Destroy(gameObject);
    //        };
    //        moving.GoTo(TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(SceneController.Instance.enterDungeon.transform)), completeCallback);
    //    }
    //    if (currentState == State.Die)
    //    {
    //        lookingFor.loockingFor = false;
    //        moving.isGoing = false;
    //        Instantiate(p_meat, TileDataTool.DetectLocation(transform).transform);
    //        TileDataTool.DetectLocation(transform).isResource = true;
    //        TileDataTool.DetectLocation(transform).GetComponent<Collider2D>().enabled = true;
    //    }
    //}

    public void Communicate(Stranger _stranger)
    {

    }

    //private bool DecideLeaveOrNot()
    //{
    //    if (currentStrangerType == StrangerType.Knight)
    //    {
    //        if (Random.Range(0, 10) > 8)
    //        {
    //            SetState(State.Leaving);
    //            return true;
    //        }

    //    }
    //    else if (currentStrangerType == StrangerType.Miner)
    //    {
    //        if (Random.Range(0, 10) > 4)
    //        {
    //            SetState(State.Leaving);
    //            return true;
    //        }
    //    }
    //    else if (currentStrangerType == StrangerType.Thief)
    //    {
    //        if (Random.Range(0, 10) > 6)
    //        {
    //            SetState(State.Leaving);
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //public void Die()
    //{
    //    SetState(State.Die);
    //}
}
