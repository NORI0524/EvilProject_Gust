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
        IDLE,    // 待機
        DAMAGE,  // 被弾
        DEATH    // 死亡
    }

    public EnemyAIState aiState = EnemyAIState.WAIT;
    public EnemyAIState nextState = EnemyAIState.WAIT;

    private bool wait = false;  // 思考を停止するかどうか
    private bool isAIStateRunning = false;  // AITimerが動作しているかどうか

    private bool discover = false;
    private bool damage = false;
    private bool approach = false;  // 接近しているかどうか
    private bool attack = false;    // 攻撃(接近したら)

    private bool endDamageAnimation = true; // 被弾アニメーションが終了しているかどうか
    private bool endAttackAnimation = true; // 攻撃アニメーションが終了しているかどうか

    private SphereCollider armCollider;
    private HpComponent hp;

    private void Start()
    {
        armCollider = GetComponentInChildren<SphereCollider>();
        hp = GetComponent<HpComponent>();
    }
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
        if (aiState!=EnemyAIState.DEATH)
        {
            if (wait)
            {
                nextState = EnemyAIState.WAIT;
                wait = false;
                return;
            }
            // 追いかける
            if (aiState != EnemyAIState.CHASE && discover && !damage && endDamageAnimation && endAttackAnimation && !attack)
            {
                nav.StartNav();
                // アニメーション
                animator.SetBool("Idle", false);
                animator.SetTrigger("Running");
                nextState = EnemyAIState.CHASE;    // 敵を見つけたら追いかける処理
                Debug.Log("CHASE：追いかける");
            }
            // 移動
            else if (aiState != EnemyAIState.MOVE && !discover && !damage && !attack && endDamageAnimation && endAttackAnimation)
            {
                nav.StartNav();
                // アニメーション
                animator.SetTrigger("Walking");
                nextState = EnemyAIState.MOVE;
                Debug.Log("MOVE：ステージ内を移動");
            }
            // 被弾
            else if (aiState != EnemyAIState.DAMAGE && damage)
            {
                // 移動処理を停止
                nav.EndNav();
                // アニメーション
                animator.SetTrigger("Damage");
                endDamageAnimation = false;
                nextState = EnemyAIState.DAMAGE;
                Debug.Log("DAMAGE：武器に当たった");
            }
            // 攻撃
            else if (attack && endAttackAnimation)
            {
                nav.EndNav();
                animator.SetTrigger("Attack");
                endAttackAnimation = false;

                armCollider.enabled = true;
                Invoke("ColliderReset", 0.3f);

                nextState = EnemyAIState.ATTACK;
                Debug.Log("接近したので攻撃");
            }
        }
        // 死亡
        if (hp.IsDead() && aiState != EnemyAIState.DEATH)
        {
            nav.EndNav();
            animator.SetTrigger("Death");
            nextState = EnemyAIState.DEATH;
        }
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
            case EnemyAIState.ATTACK:
                // 攻撃処理
                break;
        }

        if (Input.GetKey(KeyCode.S)) {
            Debug.Log(aiState);
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

    public void EndAttack()
    {
        endAttackAnimation = true;
    }

    public void Attack()
    {
        attack = true;
    }

    public void nAttack()
    {
        attack = false;
    }

    public void Depart()
    {
        approach = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            damage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            damage = false;
        }
    }

    private void ColliderReset()
    {
        armCollider.enabled = false;
    }
}
