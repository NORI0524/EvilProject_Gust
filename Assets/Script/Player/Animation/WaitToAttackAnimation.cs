using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToAttackAnimation : StateMachineBehaviour
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
        Debug.Log("Attack State !!");
        playerState.AddBit(PlayerState.Attack);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
