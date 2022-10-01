using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BuildData", menuName = "ScriptableObjects/BuildData")]
public class BuildData : ScriptableObject
{
    public string buildName;
    public Sprite buildSprite;
    public Trap trap;
    public Resource.Type[] needResources = new Resource.Type[3];
    public int[] needAmountResources = new int[3];
}
