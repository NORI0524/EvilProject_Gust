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
    private EnemyUIGenerator ui;

    [SerializeField] private GameObject attackEffect;
    [SerializeField] private bool effect;

    [SerializeField] private float attackAnimationTime = 1.5f;
    [SerializeField] private float attackColliderStartTime = 0.5f;
    [SerializeField] private float attackColliderLifeTime = 1.0f;
    [SerializeField] private float attackColTime = 120.0f;

    float animationWait = 0.0f;
    float colliderWait = 0.0f;
    int attackcount = 0;

    private void Start()
    {
        armCollider = GetComponentInChildren<SphereCollider>();
        HitCollider = GetComponent<CapsuleCollider>();
        hp = GetComponent<HpComponent>();
        ui = GetComponent<EnemyUIGenerator>();
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
        //Debug.Log("MOVE：ステージ内を移動");
    }

    protected void AIMainRoutine()
    {
        if (hp.Hp > 0)
        {
            if (wait)
            {
                EndEffect(); ColliderReset(); nAttack();
                HitCollider.enabled = true;
                nextState = EnemyAIState.WAIT;
                wait = false;
                return;
            }
            // 追いかける
            if (aiState != EnemyAIState.CHASE && discover && !damage && endDamageAnimation && endAttackAnimation && !attack)
            {
                EndEffect();
                HitCollider.enabled = true;
                nav.StartNav();
                // アニメーション
                animator.SetBool("Idle", false);
                animator.SetTrigger("Running");
                nextState = EnemyAIState.CHASE;
                ////Debug.Log("CHASE：追いかける");
            }
            // 移動
            if (aiState != EnemyAIState.MOVE && !discover && !damage && !attack && endDamageAnimation && endAttackAnimation)
            {
                HitCollider.enabled = true;
                nav.StartNav();
                // アニメーション
                animator.SetTrigger("Walking");
                nextState = EnemyAIState.MOVE;
                //Debug.Log("MOVE：ステージ内を移動");
            }
            // 被弾
            if (damage)
            {
                if (endDamageAnimation)
                {
                    animator.SetTrigger("Damage");
                }
                animationWait = 0.0f; colliderWait = 0.0f;
                nav.EndNav();
                EndAttack();
                StartHit();
                EndEffect();
                // アニメーション
                //Debug.Log(this.gameObject.name + " : " + hp.Hp);
                nextState = EnemyAIState.DAMAGE;
                //Debug.Log("DAMAGE：武器に当たった");
            }
            // 攻撃
            if (aiState != EnemyAIState.DAMAGE && aiState != EnemyAIState.ATTACK && attack && endAttackAnimation && endDamageAnimation)
            {
                HitCollider.enabled = true;
                attackcount++;
                animationWait = 0.0f;
                nav.EndNav();
                StartAttack();
                animator.SetTrigger("Attack");
                nextState = EnemyAIState.ATTACK;
                //Debug.Log("接近したので攻撃");
            }
        }
        // 死亡
        if (hp.IsDead() && aiState != EnemyAIState.DEATH)
        {
            animator.SetTrigger("Death");
            nav.EndNav();
            EndAttack();
            EndHit();
            ColliderReset();
            HitCollider.enabled = false;
            DestroyUI();
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
                animationWait++;
                colliderWait++;
                if (animationWait > 50.0f) { ReturnWaitState();  } //
                if (colliderWait > 5.0f) { HitCollider.enabled = true;EndHitAnimation(); }
                // ダメージ処理
                break;
            case EnemyAIState.ATTACK:
                animationWait++;
                if (animationWait > 230.0f) { ReturnWaitState(); } //
                if (animationWait > attackColTime) { ColliderReset(); }    // 攻撃コライダーON
                // 攻撃処理
                break;
        }

        //if (Input.GetKeyUp(KeyCode.Space)) { Debug.Log(animationWait); }
    }

    public void Discover()
    {
        discover = true;
    }

    public void EndDiscover()
    {
        discover = false;
    }

    // ダメージ処理
    public void StartHit()
    {
        endDamageAnimation = false;
        HitCollider.enabled = false;

        Invoke("EndHit", 1.0f);
    }

    public void EndHit()
    {
        damage = false;
        //HitCollider.enabled = true;
    }

    public void EndHitAnimation()
    {
        endDamageAnimation = true;
    }

    public void StartAttack()
    {
        endAttackAnimation = false;
        if (effect) attackEffect.SetActive(true);
        Invoke("ColliderStart", attackColliderStartTime);
        Invoke("EndAttack", attackAnimationTime);
    }

    public void EndAttack()
    {
        endAttackAnimation = true;
        //Debug.Log(attack);
    }

    public void ReturnWaitState()
    {
        nextState = EnemyAIState.WAIT;
        animationWait = 0.0f;
    }

    public void EndEffect()
    {
        if (effect) attackEffect.SetActive(false);
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

    public void DestroyUI()
    {
        ui.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            if (hp.IsDamageTrigger())
            {
                damage = true;
                animationWait = 0.0f;
            }
        }
    }

    private void ColliderStart()
    {
        // 攻撃開始
        armCollider.enabled = true;
        //Invoke("ColliderReset", attackColliderLifeTime);
    }
    private void ColliderReset()
    {
        // 攻撃終了
        armCollider.enabled = false;
    }
}
