using UnityEngine;
using System.Collections;
using System;

public class AttackState : BaseState, ICanAttack
{
    [SerializeField] private float attackReloadTime;

    public AttackState(Unit _unit) : base(_unit)
    {
    }

    public GameObject AttackingTarget { get; set; }

    public float AttackReloadTime => attackReloadTime;

    public TileData AttackingTargerPos { get { return TileDataTool.DetectLocation(AttackingTarget.transform); }}

    public override void Tick()
    {

    }
}

public static class AttackProcessor
{
    public static void Process(IHaveStats _attacker, IHaveHealth _target)
    {
        int _amount = CalculateAttackAmount(_attacker);
    }

    public static int CalculateAttackAmount(IHaveStats _attacker)
    {
        return _attacker.STR * 2;
    }

    public static void ProcessAttack(IHaveHealth _target, int _amount)
    {
        _target.ModifyHealth(_amount);
    }
}

public interface ICanAttack
{
    GameObject AttackingTarget { get; set; }
    float AttackReloadTime { get; }
    TileData AttackingTargerPos { get;}
}

//public void AttackTarget(GameObject _go)
//{
//    prevState = currentState;
//    fightingWith = _go;
//    lookingFor.loockingFor = false;
//    //if (DecideLeaveOrNot())
//    //    return;
//    targerPos = TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(fightingWith.transform), TileDataTool.DetectLocation(transform));
//    Attack();
//}

//private void Attack()
//{
//    if (TileDataTool.Heuristic(TileDataTool.DetectLocation(transform), TileDataTool.DetectLocation(fightingWith.transform)) <= 1)
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
//        targerPos = TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(fightingWith.transform), TileDataTool.DetectLocation(transform));
//        moving.GoTo(targerPos, completeCallback);
//    }
//}

//public void AttackAnimOver()
//{
//    fightingWith.GetComponent<Stats>().ToDamage(stats.Damage);
//    if (fightingWith.GetComponent<Stats>().isDead)
//    {
//        if (fightingWith.GetComponent<Cueen>() != null)
//        {
//            goalIsComplete = true;
//            SetState(State.Leaving);
//        }
//        else
//        {
//            currentState = prevState;
//            lookingFor.loockingFor = true;
//        }
//        fightingWith = null;
//        return;
//    }
//    else
//        CoroutineActions.ExecuteAction(attackReloadTime, () => { Attack(); });

//    SetState(State.IsIdle);
//}
