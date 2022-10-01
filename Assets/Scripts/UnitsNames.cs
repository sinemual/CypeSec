using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsNames : MonoBehaviour
{
    public string[] cimNames;
    public bool[] freeCimNames;
    public string[] strangersNames;

    private int[] shuffleArray;

    private void Start()
    {
        freeCimNames = new bool[cimNames.Length];
        for (int i = 0; i < freeCimNames.Length; i++)
            freeCimNames[i] = true;
        GetShuffle();
    }

    public string GetName()
    {
        for (int i = 0; i < freeCimNames.Length; i++)
            if (freeCimNames[shuffleArray[i]])
            {
                freeCimNames[shuffleArray[i]] = false;
                return cimNames[shuffleArray[i]];
            }
        return "Null";
    }

    private void GetShuffle()
    {
        shuffleArray = new int[cimNames.Length];
        for (int i = 0; i < cimNames.Length; i++)
            shuffleArray[i] = i;
        Utility.ShuffleArray(shuffleArray);
    }
}
