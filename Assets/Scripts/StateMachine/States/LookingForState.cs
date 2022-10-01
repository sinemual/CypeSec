using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookingForState : BaseState
{
    public string currentLookingFor { get; set; }
    [SerializeField]
    public TileData destinationPoint { get; set; }
    public UnityAction OnFinish { get; set; }

    private bool isLookingFor;

    public LookingForState(Unit _unit) : base()
    {
        unit = _unit;
    }

    public override void Enter()
    {
        if (unit.US.Memory.lookingForTag != "")
        {
            isLookingFor = true;
            LookingFor();
        }
    }

    private void LookingFor()
    {
        TileData current = TileDataTool.GetNierNeighborGround(TileDataTool.DetectLocation(unit.transform), true, true);
        Queue<TileData> frontiers = new Queue<TileData>();
        List<TileData> came_from = new List<TileData>();
        frontiers.Enqueue(current);

        if (current == null)
        {
            Debug.Log("Exit LookingFor");
            unit.SM.StateOver();
            //  GetComponent<NullCatcher>().LookingForCatcher();
        }


        while (frontiers.Count > 0)
        {
            if (!isLookingFor)
                break;
            current = frontiers.Dequeue();

            if (current.tileType == TileData.TileType.SWall && !unit.US.Memory.objectsInMemory.Contains(current.gameObject))
            {
                UnityAction _onHasCome = () => { LookingFor(); };
                destinationPoint = TileDataTool.GetNierNeighborGround(current);
                unit.US.Moving.GoTo(destinationPoint, _onHasCome);
                break;
            }

            foreach (TileData next in current.neighbors)
                if (next.tileType != TileData.TileType.Wall && !came_from.Contains(next))
                {
                    frontiers.Enqueue(next);
                    came_from.Add(next);
                }
        }
    }

    public override void Tick()
    {
        if (isLookingFor)
        {
            if (unit.US.Memory.memoryImprint != null && unit.US.Memory.memoryImprint.CompareTag(unit.US.Memory.lookingForTag))
            {
                unit.US.Memory.foundObject = unit.US.Memory.memoryImprint;
                unit.SM.StateOver();
                isLookingFor = false;
                Debug.Log("Found LookingFor");
            }
        }
    }
}

public interface ILookingFor
{
    string ObjectTag { get; set; }
}
