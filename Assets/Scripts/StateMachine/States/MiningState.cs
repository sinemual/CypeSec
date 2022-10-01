using System;
using UnityEngine;
using UnityEngine.Events;

public class MiningState : BaseState
{
    private Unit miner;
    public MiningState(Unit _miner) : base(_miner)
    {
        miner = _miner;
    }

    public override void Enter()
    {
        base.Enter();

        UnityAction _onMiningComplete = () =>
        {
            UnityAction _onHasComeToStorage = () =>
            {
                StorageManager.Instance.PutResource(miner.US.Hands.InHands);
                miner.US.Hands.Drop();
                miner.SM.StateOver();
            };
            miner.US.Moving.GoTo(StorageManager.Instance.loadUnloadPoint, _onHasComeToStorage);
        };

        UnityAction _onHasComeToMine = () =>
        {
            miner.US.Memory.foundObject.GetComponent<Resource>().Mine(miner, _onMiningComplete);
        };
        miner.US.Moving.GoTo(TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(miner.US.Memory.foundObject.transform)), _onHasComeToMine);
    }

    public override void Tick()
    {

    }
}
