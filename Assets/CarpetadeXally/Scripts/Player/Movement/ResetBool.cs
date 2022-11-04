using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{
    public string isInteractingBool;
    public string isUsingRootMotionBool;
    //public string isInvulnerable;

    public bool isInteractingStatus;
    //public bool isInvulnerableStatus;
    public bool isUsingRootMotionStatus;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(isInteractingBool, isInteractingStatus);
        animator.SetBool(isUsingRootMotionBool, isInteractingStatus);
        //animator.SetBool(isInvulnerable, isInvulnerableStatus);
    }  
 
}
