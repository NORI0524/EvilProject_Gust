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

    [SerializeField] private bool effect;
    [SerializeField] private GameObject[] attackEffect;    // 目的地の配列

    [SerializeField] private int attackValue = 1;
    private int attackCount = 0;

    [SerializeField] private float attackAnimationTime = 1.5f;
    [SerializeField] private float attackColliderStartTime = 0.5f;
    [SerializeField] private float attackColliderLifeTime = 1.0f;
    [SerializeField] private float attackColTime = 120.0f;

    float animationWait = 0.0f;
    float colliderWait = 0.0f;

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
                animator.SetBool("Idle", false);
                animator.SetTrigger("Running");
                nextState = EnemyAIState.CHASE;
            }
            // 移動
            if (aiState != EnemyAIState.MOVE && !discover && !damage && !attack && endDamageAnimation && endAttackAnimation)
            {
                HitCollider.enabled = true;
                nav.StartNav();
                animator.SetTrigger("Walking");
                nextState = EnemyAIState.MOVE;
            }
            // 被弾
            if (damage)
            {
                var attackS = GetComponent<Attack>();
                if (this.gameObject.name == "Juggernaut") attackS.EndBackStep();
                if (endDamageAnimation)
                {
                    animator.SetTrigger("Damage");
                }
                animationWait = 0.0f; colliderWait = 0.0f;
                nav.EndNav();
                EndAttackAnimation();
                StartHit();
                EndEffect();
                nextState = EnemyAIState.DAMAGE;
            }
            // 攻撃
            if (aiState != EnemyAIState.DAMAGE && aiState != EnemyAIState.ATTACK && attack && endAttackAnimation && endDamageAnimation)
            {
                HitCollider.enabled = true; //  Attack
                animationWait = 0.0f;   // これもAttack(アニメーションイベントでもいい)で管理
                nav.EndNav();   // Navigation
                StartAttack();  // Attack
                // AttackCountなどで攻撃のバリエーションを設定、ランダムで取得(はじめはカウントアップ制でいいかも)
                switch (attackCount)
                {
                    case 0:
                        animator.SetTrigger("Attack");  //Attack
                        break;
                    case 1:
                        animator.SetTrigger("Attack2");
                        break;
                }
                attackCount++;  //Attack
                if (attackCount > attackValue - 1) { attackCount -= attackValue; }  // Attack
                nextState = EnemyAIState.ATTACK;
            }
        }
        // 死亡
        if (hp.IsDead() && aiState != EnemyAIState.DEATH)
        {
            animator.SetTrigger("Death");
            nav.EndNav();
            EndAttackAnimation();
            EndHit();
            ColliderReset();
            HitCollider.enabled = false;
            DestroyUI();
            nextState = EnemyAIState.DEATH;

            Invoke("DestroyEnemy", 10.0f);
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
                if (animationWait > 50.0f) { ReturnWaitState(); }
                if (colliderWait > 5.0f) { HitCollider.enabled = true; EndHitAnimation(); }
                break;
            case EnemyAIState.ATTACK:
                animationWait++;
                if (animationWait > 230.0f) { ReturnWaitState(); }
                if (animationWait > attackColTime) { ColliderReset(); }
                break;
        }
        if (Input.GetKeyDown(KeyCode.Return)) {  }
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
    }

    public void EndHitAnimation()
    {
        endDamageAnimation = true;
    }

    public void StartAttack()
    {
        endAttackAnimation = false;
        if (effect) attackEffect[0].SetActive(true);
        Invoke("ColliderStart", attackColliderStartTime);
        //Invoke("EndAttackAnimation", attackAnimationTime);
    }

    public void EndAttackAnimation()
    {
        endAttackAnimation = true;
    }

    public void ReturnWaitState()
    {
        nextState = EnemyAIState.WAIT;
        animationWait = 0.0f;
    }

    public void EndEffect()
    {
        if (effect) attackEffect[0].SetActive(false);
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
            Debug.Log("武器に当たりました");
            if (hp.IsDamageTrigger())
            {
                damage = true;
                animationWait = 0.0f;
            }
            else { Debug.Log("だめだ"); }
        }
    }

    private void ColliderStart()
    {
        // 攻撃開始
        armCollider.enabled = true;
    }
    private void ColliderReset()
    {
        // 攻撃終了
        armCollider.enabled = false;
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
