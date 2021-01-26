using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnemyDirection : StateMachineBehaviour
{
    //[SerializeField, Range(0.0f, 10.0f)] private float SearchDistance = 1.0f;

    [SerializeField] float rotateSpeed = 1.0f;
    private GameObject[] enemys = null;
    private Vector3 targetEnemyVec;
    private GameObject player = null;

    private SearchObjectComponent searchObject = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            player = animator.gameObject;
            searchObject = player.GetComponentInChildren<SearchObjectComponent>();
        }

        //最も近い敵を検索
        var enemy = searchObject.ClosestObject;
        if (enemy == null) return;
        targetEnemyVec = enemy.transform.position - player.transform.position;

        if (targetEnemyVec == Vector3.zero) return;

        //敵の方向を向くように回転（yの移動量は無視）
        targetEnemyVec.y = 0.0f;

        var moveQua = Quaternion.LookRotation(targetEnemyVec);
        player.transform.rotation = moveQua;
    }

    private IEnumerable Rotate()
    {
        var time = 0.0f;
        var moveQua = Quaternion.LookRotation(targetEnemyVec);
        while (time < 1.0f)
        {
            time = Time.deltaTime * rotateSpeed;
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, moveQua, time);
            yield return null;
        }
    }
}
