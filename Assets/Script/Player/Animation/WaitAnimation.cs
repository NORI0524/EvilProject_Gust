using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaitAnimation : StateMachineBehaviour
{
    private BitFlag playerState = null;

    private WeaponManager weaponManager = null;

    private PlayerController playerCtrl = null;

    private void Awake()
    {
        playerState = new BitFlag();
        var obj = GameObject.Find("unitychan_dynamic");
        playerCtrl = obj.GetComponent<PlayerController>();
        playerState = playerCtrl.state;
        weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Wait State !!");
        playerState.AddBit(PlayerState.Wait);
        playerState.FoldBit(PlayerState.Attack | PlayerState.Attack2 | PlayerState.Attack3 | PlayerState.Attack_heavy | PlayerState.Attack_heavy2 | PlayerState.Attack_heavy3);
        playerState.FoldBit(PlayerState.Avoid);
        playerCtrl.StartCoroutine(weaponManager.ChangeWeapon(WeaponType.None));
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
