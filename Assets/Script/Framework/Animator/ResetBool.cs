using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResetTiming
{
    Enter,
    Exit,
}
public class ResetBool : StateMachineBehaviour
{
    [SerializeField] string[] boolParameterNames = null;
    [SerializeField] ResetTiming timing = ResetTiming.Enter;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timing != ResetTiming.Enter) return;
        foreach (var parameterName in boolParameterNames)
        {
            if (animator.CheckExistsParameter(parameterName))
            {
                animator.SetBool(parameterName, false);
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timing != ResetTiming.Exit) return;
        foreach (var parameterName in boolParameterNames)
        {
            if (animator.CheckExistsParameter(parameterName))
            {
                animator.SetBool(parameterName, false);
            }
        }
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
