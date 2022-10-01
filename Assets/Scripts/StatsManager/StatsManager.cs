using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public PossibleStatsData cimStatsPossibleData;
    public PossibleStatsData knightStatsPossibleData;

    public void GiveStats(Unit _unit)
    {
        if (_unit.CompareTag("Cim"))
        {
            //_unit.ST.STR = 
        }
    }
}
