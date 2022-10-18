using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttack : State
{

    public float stateDurationTime = 25f;
    public float currentStateTime;
    public override State RunCurrentState()
    {

        currentStateTime = currentStateTime - Time.deltaTime;
        Debug.Log(currentStateTime);

        return this;
    }

    
}
