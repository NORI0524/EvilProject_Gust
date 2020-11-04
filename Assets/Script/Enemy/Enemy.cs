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

    private bool discover = false;  // 発見しているかどうか
    private bool damage = false;    // 被弾しているかどうか
    private bool approach = false;  // 接近しているかどうか
    private bool attack = false;    // 攻撃(接近したら)

    private bool endDamageAnimation = true; // 被弾アニメーションが終了しているかどうか
    private bool endAttackAnimation = true; // 攻撃アニメーションが終了しているかどうか

    private SphereCollider armCollider;
    private CapsuleCollider HitCollider;
    private HpComponent hp;

    private void Start()
    {
        armCollider = GetComponentInChildren<SphereCollider>();
        HitCollider = GetComponent<CapsuleCollider>();
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
        if (!hp.IsDead())
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
                nextState = EnemyAIState.CHASE;
                ////Debug.Log("CHASE：追いかける");
            }
            // 移動
            else if (aiState != EnemyAIState.MOVE && !discover && !damage && !attack && endDamageAnimation && endAttackAnimation)
            {
                nav.StartNav();
                // アニメーション
                animator.SetTrigger("Walking");
                nextState = EnemyAIState.MOVE;
                ////Debug.Log("MOVE：ステージ内を移動");
            }
            // 被弾
            else if (endDamageAnimation && damage)
            {
                EndAttack();
                StartHit();

                nav.EndNav();
                // アニメーション
                //animator.SetTrigger("Damage");
                animator.Play("Damage");
                ////Debug.Log(this.gameObject.name + " : " + hp.Hp);
                nextState = EnemyAIState.DAMAGE;
                ////Debug.Log("DAMAGE：武器に当たった");
            }
            // 攻撃
            else if (attack && endAttackAnimation && endDamageAnimation)
            {
                StartAttack();
                nav.EndNav();
                animator.SetTrigger("Attack");

                if (!armCollider) { Debug.Log("攻撃用コライダーが見つかりません"); }
                Invoke("ColliderStart", 0.5f);

                nextState = EnemyAIState.ATTACK;
                ////Debug.Log("接近したので攻撃");
            }
        }
        // 死亡
        if (hp.IsDead() && aiState != EnemyAIState.DEATH)
        {

            EndAttack();
            EndHit();
            HitCollider.enabled = false;
            nav.EndNav();
            animator.Play("Death");
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

    }

    public void Discover()
    {
        discover = true;
    }

    public void EndDiscover()
    {
        discover = false;
    }

    public void StartHit()
    {
        endDamageAnimation = false;
        HitCollider.enabled = false;
    }

    public void EndHit()
    {
        endDamageAnimation = true;
        HitCollider.enabled = true;
    }

    public void StartAttack()
    {
        endAttackAnimation = false;
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

    public void DamageReset()
    {
        damage = false;
    }

    private void ColliderStart()
    {
        armCollider.enabled = true;
        Debug.Log("攻撃開始");
        Invoke("ColliderReset", 0.2f);
    }
    private void ColliderReset()
    {
        armCollider.enabled = false;
        Debug.Log("攻撃終了");
    }
}
