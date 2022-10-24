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

        bossRenderer.material.color = Color.blue;
        if( stateTimeFinished == false)
        {
            currentStateTime -= Time.deltaTime;
        }
        

        Debug.Log(currentStateTime);
        Debug.Log("Moving Attack");

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

    
}
