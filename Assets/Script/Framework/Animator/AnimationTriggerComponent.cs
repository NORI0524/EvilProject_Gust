using System;
using System.Collections;
using System.Collections.Generic;
using UniJSON;
using UnityEngine;
using UnityEngine.Events;

public class AnimationTriggerComponent : StateMachineBehaviour
{
    [Serializable] public class CallBackFunction : UnityEvent<GameObject> { }


    //アニメーションの開始
    [field: SerializeField] public CallBackFunction enterFunction { get; set; }

    //アニメーション中
    [field: SerializeField] public CallBackFunction stayFunction { get; set; }
    //アニメーションの終了
    [field: SerializeField] public CallBackFunction exitFunction { get; set; }

    //タグを複数設定できるようにリスト
    [SerializeField] List<string> tagList = new List<string>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (enterFunction.GetPersistentEventCount() == 0) return;
        enterFunction.Invoke(animator.gameObject);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stayFunction.GetPersistentEventCount() == 0) return;
        stayFunction.Invoke(animator.gameObject);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (exitFunction.GetPersistentEventCount() == 0) return;
        exitFunction.Invoke(animator.gameObject);
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
