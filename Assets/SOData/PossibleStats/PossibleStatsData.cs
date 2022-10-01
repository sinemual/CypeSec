using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PossibleStatsData", menuName = "ScriptableObjects/PossibleStatsData")]
public class PossibleStatsData : ScriptableObject
{
    [NaughtyAttributes.MinMaxSlider(1, 10)] public Vector2 STR;
    [NaughtyAttributes.MinMaxSlider(1, 10)] public Vector2 AGL;
    [NaughtyAttributes.MinMaxSlider(1, 10)] public Vector2 INT;
    [NaughtyAttributes.MinMaxSlider(1, 10)] public Vector2 Speed;
}
