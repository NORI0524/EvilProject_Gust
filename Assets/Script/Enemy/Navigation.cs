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
    [SerializeField] private float moveSpeed = 1.0f; // 移動速度
    [SerializeField] public GameObject[] Target;    // 目的地の配列
    private int targetCount = 0; // 現在の目的地番号

    //CHASE
    [SerializeField] Transform Player = default;    // プレイヤーの座標
    [SerializeField] float dist = 7.0f;    // 索敵範囲(範囲外に出たら追尾終了)
    float approarchDist = 2.0f; // この距離まで近づいたら止まる

    // フラグ
    bool tracking = false;  // プレイヤーを追尾しているかどうか
    bool searching = false; // 見失った時にSearchするかどうか
    bool moving = false;    // ステージ内を移動するかどうか

    private Vector3 saveTargetPos;  // 見失う直前のプレイヤーの座標を保存

    private bool haveTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        e_con = GetComponent<Enemy>();

        if (Target.Length > 0) { haveTarget = true; } else { haveTarget = false; }
    }

    void Update()
    {
        if (tracking)
        {
            float distance = Vector3.Distance(this.transform.position, Player.transform.position);
            if (distance >= dist)
            {
                e_con.EndDiscover();
            }
            //if (distance <= approarchDist) { e_con.Attack(); EndNav(); } else { e_con.nAttack(); }
        }

        if (haveTarget)
        {
            if (moving)
            {
                // 目的地にたどり着いたら次の目的地を設定する
                if (Mathf.Abs(this.transform.position.x - Target[targetCount].transform.position.x) < 5
                    && Mathf.Abs(this.transform.position.z - Target[targetCount].transform.position.z) < 5)
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

        if (Input.GetKey(KeyCode.M)) agent.isStopped = true;
    }

    // 追尾
    public void StartTracking()
    {
        tracking = true;
        moving = false;

        agent.speed = 16.0f;
        agent.angularSpeed = 150;
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

        agent.speed = moveSpeed;
        agent.angularSpeed = 120;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            if (haveTarget)
            {
                // 目的地の座標を設定
                agent.SetDestination(Target[targetCount].transform.position);
            }
            else
            {
                agent.isStopped = true;
            }
        }
    }

    // 移動開始
    public void StartNav()
    {
        agent.isStopped = false;
    }

    // 移動を終了
    public void EndNav()
    {
        agent.isStopped = true;
    }
}
