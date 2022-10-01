using UnityEngine;
using System.Collections;

public class UnitSystems : MonoBehaviour
{
    private Memory memory;
    private Vision vision;
    private Hunger hunger;
    private Moving moving;
    private Hands hands;

    //SYSTEMS
    public Memory Memory { get => memory; private set => memory = value; }
    public Vision Vision { get => vision; private set => vision = value; }
    public Hunger Hunger { get => hunger; private set => hunger = value; }
    public Moving Moving { get => moving; private set => moving = value; }
    public Hands Hands { get => hands; private set => hands = value; }

    private void Awake()
    {
        if (GetComponent<Memory>() != null)
            memory = GetComponent<Memory>();
        if (GetComponent<Vision>() != null)
            vision = GetComponent<Vision>();
        if (GetComponent<Hunger>() != null)
            hunger = GetComponent<Hunger>();
        if (GetComponent<Moving>() != null)
            moving = GetComponent<Moving>();
        if (GetComponent<Hands>() != null)
            hands = GetComponent<Hands>();
    }
}
