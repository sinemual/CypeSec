using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunger : MonoBehaviour
{
    public int food = 40;
    public float eatsTime = 10.0f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void Hungry()
    {
        //if (food <= 0)
        //    cim.Die();

        if (timer > eatsTime)
        {
            food--;
            //if (food < 20)
            //    currentState = State.lookingFor;
            timer = 0.0f;
        }
    }
}
