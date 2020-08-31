using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaitAnimation : StateMachineBehaviour
{
    private BitFlag playerState = null;

    private void Awake()
    {
        playerState = new BitFlag();
        playerState = GameObject.Find("unitychan_dynamic").GetComponent<Player>().state;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Wait State !!");
        playerState.AddBit(PlayerState.Wait);
        playerState.FoldBit(PlayerState.Attack | PlayerState.Attack2 | PlayerState.Attack3);
        playerState.FoldBit(PlayerState.Avoid);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}
}
