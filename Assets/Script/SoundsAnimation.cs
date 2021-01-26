using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsAnimation : StateMachineBehaviour
{
    [SerializeField] private AudioClip animationAudio = null;

    [SerializeField] private bool isPlayEnterAnimation = false;

    [SerializeField, Range(0.0f, 1.0f)] private List<float> delayList = null;

    [SerializeField, Range(0.0f, 1.0f)] private float volume = 1.0f;

    AudioSource audioSource = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioSource = animator.gameObject.GetComponent<AudioSource>();
        if(isPlayEnterAnimation)
        {
            audioSource.PlayOneShot(animationAudio, volume);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(var delay in delayList)
        {
            var deltaTime = Mathf.Abs(stateInfo.normalizedTime - delay);
            //Debug.Log(deltaTime);
            if(deltaTime < 0.005f)
            {
                audioSource.PlayOneShot(animationAudio, volume);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}
}
