using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum AttackState
{
    Normal_First,
    Normal_Second,
    Normal_Third,
    Heavy_First,
    Heavy_Second,
    Heavy_Third,
}

public class AtttackAnimation : StateMachineBehaviour
{
    [SerializeField] AttackState state = AttackState.Normal_First;

    private BitFlag playerState = null;

    private void Awake()
    {
        playerState = new BitFlag();
        playerState = GameObject.Find("unitychan_dynamic").GetComponent<Player>().state;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(state)
        {
            case AttackState.Normal_First:
                playerState.AddBit(PlayerState.Attack);
                break;
            case AttackState.Normal_Second:
                playerState.AddBit(PlayerState.Attack2);
                break;
            case AttackState.Normal_Third:
                playerState.AddBit(PlayerState.Attack3);
                break;
            case AttackState.Heavy_First:
                playerState.AddBit(PlayerState.Attack_heavy);
                break;
            case AttackState.Heavy_Second:
                playerState.AddBit(PlayerState.Attack_heavy2);
                break;
            case AttackState.Heavy_Third:
                playerState.AddBit(PlayerState.Attack_heavy3);
                break;
            default:
                break;
        }

        Debug.Log(state);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
