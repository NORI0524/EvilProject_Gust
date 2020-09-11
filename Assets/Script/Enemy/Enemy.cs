using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] Monitor monitor;
    [SerializeField] Navigation nav;
    [SerializeField] Animator animator;

    public enum EnemyAIState
    {
        WAIT,   // 行動を一旦停止
        MOVE,   // 移動
        SEARCH, // 索敵(首を回して)
        CHASE,  // 追いかける
        ATTACK, // 攻撃
        IDOL,    // 待機
        DAMAGE  // 被弾
    }

    public EnemyAIState aiState = EnemyAIState.WAIT;
    public EnemyAIState nextState = EnemyAIState.WAIT;

    private bool wait = false;  // 思考を停止するかどうか
    private bool isAIStateRunning = false;  // AITimerが動作しているかどうか

    private bool discover = false;
    private bool damage = false;
    private bool approach = false;  // 接近しているかどうか

    private bool endDamageAnimation = true;

    protected void InitAI()
    {

    }

    protected void SetAI()
    {
        if (isAIStateRunning)
        {
            return;
        }

        InitAI();
        AIMainRoutine();

        aiState = nextState;

        StartCoroutine("AITimer");
    }

    IEnumerator AITimer()
    {
        // 処理
        isAIStateRunning = true;

        // 1フレーム停止
        yield return null;

        // 再開後の処理
        isAIStateRunning = false;
    }

    protected void Wait()
    {
        nextState = EnemyAIState.MOVE;
        Debug.Log("MOVE：ステージ内を移動");
    }

    protected void AIMainRoutine()
    {
        if (wait)
        {
            nextState = EnemyAIState.WAIT;
            wait = false;
            return;
        }
        if (aiState != EnemyAIState.CHASE && discover && !damage && endDamageAnimation)
        {
            // アニメーション
            animator.SetTrigger("Running");
            nextState = EnemyAIState.CHASE;    // 敵を見つけたら追いかける処理
            Debug.Log("CHASE：追いかける");
        }
        else if (aiState != EnemyAIState.MOVE && !discover && !damage)
        {
            // アニメーション
            animator.SetTrigger("Walking");
            // 移動処理を停止
            nav.EndNav();
            nextState = EnemyAIState.MOVE;
            Debug.Log("MOVE：ステージ内を移動");
        }
        else if (aiState != EnemyAIState.DAMAGE && damage)
        {
            // アニメーション
            animator.SetTrigger("Damage");
            endDamageAnimation = false;
            // 移動処理を停止
            nav.EndNav();
            nextState = EnemyAIState.DAMAGE;
            Debug.Log("DAMAGE：武器に当たった");
        }
        /*else if (aiState != EnemyAIState.IDOL && approach)
        {
            animator.SetTrigger("Idle");
            nextState = EnemyAIState.IDOL;
        }*/
        // 追いかけている途中でターゲットを見失った場合
        // 左右に首を振って敵を探す処理
        /*else if (aiState == EnemyAIState.CHASE && !discover)
        {
            nextState = EnemyAIState.SEARCH;
        }*/
        //else { nextState = EnemyAIState.MOVE; } // 敵を見つけてなかったら移動処理
    }

    protected void Update()
    {
        SetAI();

        switch (aiState)
        {
            case EnemyAIState.WAIT:
                break;
            case EnemyAIState.MOVE:
                nav.StartMoving();
                break;
            case EnemyAIState.CHASE:
                nav.StartTracking();
                break;
            case EnemyAIState.SEARCH:
                // nav.Search();
                break;
            case EnemyAIState.DAMAGE:
                // ダメージ処理
                break;
        }
    }

    public void Discover()
    {
        discover = true;
    }

    public void EndDiscover()
    {
        discover = false;
    }

    public void EndHit()
    {
        endDamageAnimation = true;
    }

    public void Approach()
    {
        approach = true;
    }

    public void Depart()
    {
        approach = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            damage = true;
            Debug.Log(other.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            damage = false;
        }
    }

}
