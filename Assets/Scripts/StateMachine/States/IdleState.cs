using System;
using UnityEngine;
using UnityEngine.Events;

public class IdleState : BaseState
{
    public IdleState(Unit _unit) : base()
    {
        unit = _unit;
    }

    public override void Enter()
    {
        unit.US.Moving.GoTo(StorageManager.Instance.idlePoint);
    }

    public override void Tick()
    {
    }
}
