using System;

public abstract class BaseState
{
    protected Unit unit;

    protected BaseState(Unit _unit = null)
    {
        this.unit = _unit;
    }

    public abstract void Tick();

    public virtual void Enter() { }
    public virtual void HandleInputTick() { }
    public virtual void PhysicsTick() { }
    public virtual void Exit() { }
}
