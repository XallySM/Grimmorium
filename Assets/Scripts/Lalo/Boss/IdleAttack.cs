using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAttack : State
{

    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 10f;
    public float currentStateTime;
    private void Start()
    {
        currentStateTime = stateDurationTime;
        
    }
    public override State RunCurrentState()
    {
        StartCoroutine(UpdateStateTime());
        Debug.Log("Idle Attack");
        if (stateTimeFinished == true)
        {
            return NextStateToThis;
        }

        else
        {
            return this;
        }
    }

    private IEnumerator UpdateStateTime()
    {
        while (currentStateTime >= 0)
        {
            yield return new WaitForSeconds(1f);
            currentStateTime--;
            Debug.Log(currentStateTime);
        }

        yield return null;
    }
    
}
