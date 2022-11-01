using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : State
{

    public State NextStateToThis;
    public State PreviousStateToThis;

    public bool stateTimeFinished = false;

    public float stateDurationTime = 4f;
    public float currentStateTime;

    public MeshRenderer bossRenderer;

    private void Awake()
    {
        bossRenderer = GetComponentInParent<MeshRenderer>();
    }

    private void Start()
    {
        currentStateTime = stateDurationTime;
        
    }
    public override State RunCurrentState()
    {
        stateTimeFinished = false;

        IndividualStateLogic();

        Debug.Log("Laser Attack");
        Debug.Log(currentStateTime);


        if (currentStateTime <= 0)
        {
            
            stateTimeFinished = true;
        }

        if (stateTimeFinished == true)
        {
            currentStateTime = stateDurationTime;
            return NextStateToThis;
        }

        else
        {
            return this;
        }
    }

    public override void IndividualStateLogic()
    {
        bossRenderer.material.color = Color.green;

        if (stateTimeFinished == false)
        {
            currentStateTime -= Time.deltaTime;
        }
    }
}
