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
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] public GameObject[] Target;
    private int targetCount = 0;

    //CHASE
    private Transform player;
    public Transform Player
    {
        get
        {
            return player;
        }
    }
    [SerializeField] float dist = 7.0f;
    float approarchDist = 2.0f;

    // フラグ
    bool tracking = false;
    bool searching = false;
    bool moving = false;

    private Vector3 saveTargetPos;

    private bool haveTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        e_con = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Target.Length > 0) { haveTarget = true; } else { haveTarget = false; }
    }

    void Update()
    {
        if (tracking)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);
            if (distance >= dist)
            {
                e_con.EndDiscover();
            }
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

        agent.speed = moveSpeed * 1.5f;
        agent.angularSpeed = 150;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // ターゲットの座標を設定
            agent.SetDestination(player.transform.position);
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
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            agent.acceleration = 2.0f;
            agent.isStopped = false;
        }
    }

    // 移動を終了
    public void EndNav()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            agent.acceleration = 10.0f;
            agent.isStopped = true;
        }
    }
}
