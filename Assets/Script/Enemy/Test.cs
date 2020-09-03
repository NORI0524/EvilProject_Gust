using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    // NavMeshAgent取得
    NavMeshAgent agent;

    // MOVE
    [SerializeField] public GameObject[] Target;    // 目的地の配列
    private int targetCount = 0; // 現在の目的地番号

    //SEARCH
    [SerializeField] Transform Player = default;    // プレイヤーの座標
    [SerializeField] float dist = 15.0f;    // 索敵範囲(範囲外に出たら追尾終了)

    // フラグ
    bool tracking = false;  // プレイヤーを追尾しているかどうか
    public bool Tracking { get { return tracking; } set { tracking = value; } }
    bool searching = false; // 見失った時にSearchするかどうか
    public bool Searching { get { return searching; } set { searching = value; } }
    bool moving = false;    // ステージ内を移動するかどうか
    public bool Moving { get { return moving; } set { moving = value; } }

    private Vector3 saveTargetPos;  // 見失う直前のプレイヤーの座標を保存

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking)
        {
            float distance = Vector3.Distance(this.transform.position, Player.transform.position);
            if (distance >= dist) { tracking = false; }
        }
        else { moving = true; }

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


        if (Input.GetKeyDown(KeyCode.A)) { StartMoving(); }
    }

    public void StartTracking()
    {
        tracking = true;
        agent.isStopped = false;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // ターゲットの座標を設定
            agent.SetDestination(Player.position);
        }
    }

    public void StartMoving()
    {
        moving = true;
        agent.isStopped = false;
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // 目的地の座標を設定
            agent.SetDestination(Target[targetCount].transform.position);
        }
    }
}
