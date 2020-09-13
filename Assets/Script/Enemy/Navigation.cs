using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    // NavMeshAgent取得
    NavMeshAgent agent;
    Enemy e_con;

    // MOVE
    [SerializeField] public GameObject[] Target;    // 目的地の配列
    private int targetCount = 0; // 現在の目的地番号

    //CHASE
    [SerializeField]
    Transform Player = default;    // プレイヤーの座標
    [SerializeField]
    float dist = 7.0f;    // 索敵範囲(範囲外に出たら追尾終了)
    float approarchDist = 3.0f; // この距離まで近づいたら止まる

    // フラグ
    bool tracking = false;  // プレイヤーを追尾しているかどうか
    bool searching = false; // 見失った時にSearchするかどうか
    bool moving = false;    // ステージ内を移動するかどうか

    private Vector3 saveTargetPos;  // 見失う直前のプレイヤーの座標を保存

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        e_con = GetComponent<Enemy>();
    }

    void Update()
    {
        if (tracking)
        {
            float distance = Vector3.Distance(this.transform.position, Player.transform.position);
            if (distance >= dist) { e_con.EndDiscover(); }
            //if (distance <= approarchDist) { e_con.Approach(); EndNav(); } else { e_con.Depart(); }
        }

        if (moving)
        {
            // 目的地にたどり着いたら次の目的地を設定する
            if (this.transform.position.x == Target[targetCount].transform.position.x && this.transform.position.z == Target[targetCount].transform.position.z)
            {
                targetCount++;
                if (targetCount > Target.Length - 1)
                {
                    targetCount -= Target.Length;
                }
                agent.SetDestination(Target[targetCount].transform.position);
            }
        }
    }

    // 追尾
    public void StartTracking()
    {
        agent.isStopped = false;
        moving = false;

        agent.speed = 2.0f;
        agent.angularSpeed = 150;
        tracking = true;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // ターゲットの座標を設定
            agent.SetDestination(Player.transform.position);
        }
    }

    // ステージ内の移動
    public void StartMoving()
    {
        tracking = false;
        moving = true;

        agent.speed = 1.0f;
        agent.angularSpeed = 120;
        agent.isStopped = false;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // 目的地の座標を設定
            agent.SetDestination(Target[targetCount].transform.position);
        }
    }

    // 移動を終了
    public void EndNav()
    {
        agent.isStopped = true;
    }
}
