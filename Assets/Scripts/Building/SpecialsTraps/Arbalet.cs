using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbalet : MonoBehaviour
{
    public GameObject p_bolt;

    private Trap trap;
    private GameObject currentBolt;
    private bool shot;

    private void Start()
    {
        trap = GetComponent<Trap>();
        trap.onWork += Work;
    }

    private void Update()
    {
        if (shot)
            Shot();
    }

    private void Work()
    {
        currentBolt = Instantiate(p_bolt, transform);
        shot = true;
    }

    private void Shot()
    {
        Vector2 targetPos;
        if (trap.targetThroughTrap)
            targetPos = trap.currentTarget.transform.position;
        else
            targetPos = trap.currentTarget.transform.position + (transform.up * 1.5f);

        currentBolt.transform.position = Vector2.MoveTowards(currentBolt.transform.position, targetPos, Time.deltaTime);
        if (Vector2.Distance(currentBolt.transform.position, targetPos) < 0.05f)
            shot = false;
    }
}
