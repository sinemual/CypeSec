using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnitSystems)),
 RequireComponent(typeof(Stats)),
 RequireComponent(typeof(StateMachine))]
public abstract class Unit : MonoBehaviour, IHaveHealth
{
    public int Health { get; set; }
    public bool IsDead { get; set; }
    public abstract void ModifyHealth(int _amount);
    public abstract void Die();

    private UnitSystems unitSystems;
    private StateMachine stateMachine;
    private Stats stats;

    public UnitSystems US;
    public StateMachine SM;
    public Stats ST;

    private void Awake()
    {
        US = gameObject.AddComponent<UnitSystems>();
        SM = gameObject.AddComponent<StateMachine>();
        ST = gameObject.AddComponent<Stats>();
        InitializeStateMachine();
        //stateMachine.Initialize(standing);
    }

    private void InitializeStateMachine()
    {
        Dictionary<Type, BaseState> _states = new Dictionary<Type, BaseState>()
        {
            { typeof(IdleState), new IdleState(this) },
            { typeof(WanderState), new WanderState(this) },
            { typeof(AttackState), new AttackState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(LookingForState), new LookingForState(this) },
            { typeof(MiningState), new MiningState(this) },
            { typeof(BuildingState), new BuildingState(this)}
        };
        SM.InitStates(_states);
    }
}