using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour, IHaveStats
{
    public int STR { get; set; }
    public int AGL { get; set; }
    public int INT { get; set; }
    public float Speed { get; set; }
}

public interface IHaveHealth
{
    int Health { get; set; }
    bool IsDead { get; set; }
    void ModifyHealth(int _amount);
    void Die();
}

public interface IHaveStats
{
    int STR { get; }
    int AGL { get; }
    int INT { get; }
    float Speed { get; }
}
