using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAttack : State
{
    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 25f;
    public float currentStateTime;

    

    private void Start()
    {
        currentStateTime = stateDurationTime;
        
    }
    public override State RunCurrentState()
    {
        Debug.Log("Moving Attack");

        if (stateTimeFinished == true)
        {
            return NextStateToThis;
        }

        else
        {
            return this;
        }

        
    }

    
}
