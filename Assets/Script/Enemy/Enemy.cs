using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] Monitor monitor;
    [SerializeField] Navigation navigation;
    [SerializeField] Animator animator;

    [SerializeField] string enemyName;

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
    private bool GetAttack = false;    // 攻撃(接近したら)

    private bool endDamageAnimation = true; // 被弾アニメーションが終了しているかどうか
    private bool endAttackAnimation = true; // 攻撃アニメーションが終了しているかどうか

    //private SphereCollider armCollider;
    private Attack attack;
    private CapsuleCollider HitCollider;
    private HpComponent hp;
    private EnemyUIGenerator ui;

    [SerializeField] private bool effect;
    [SerializeField] private GameObject attackEffect;    // 目的地の配列

    [SerializeField] private float attackColliderStartTime = 0.5f;
    [SerializeField] private float attackColTime = 120.0f;

    float animationWait = 0.0f;
    float colliderWait = 0.0f;

    private bool invincible;

    private void Start()
    {
        //armCollider = GetComponentInChildren<SphereCollider>();
        attack = GetComponent<Attack>();
        HitCollider = GetComponent<CapsuleCollider>();
        hp = GetComponent<HpComponent>();
        ui = GetComponent<EnemyUIGenerator>();
        invincible = false;
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
    }

    protected void AIMainRoutine()
    {
        if (hp.Hp > 0)
        {
            if (wait)
            {
                EndEffect(); attack.InvalidAllColliders(); nAttack();
                HitCollider.enabled = true;
                nextState = EnemyAIState.WAIT;
                wait = false;
                return;
            }
            // 追いかける
            if (aiState != EnemyAIState.CHASE && discover && !damage && endDamageAnimation && endAttackAnimation && !GetAttack)
            {
                EndEffect();
                HitCollider.enabled = true;
                navigation.StartNav();
                animator.SetBool("Idle", false);
                animator.SetTrigger("Running");
                nextState = EnemyAIState.CHASE;
            }
            // 移動
            if (aiState != EnemyAIState.MOVE && !discover && !damage && !GetAttack && endDamageAnimation && endAttackAnimation)
            {
                HitCollider.enabled = true;
                navigation.StartNav();
                animator.SetTrigger("Walking");
                nextState = EnemyAIState.MOVE;
            }
            // 被弾
            if (damage && !invincible)
            {
                if (this.gameObject.name == "Juggernaut") attack.EndBackStep();
                if (this.gameObject.name == "quin2_beach") attack.EndJumping();
                if (endDamageAnimation)
                {
                    animator.SetTrigger("Damage");
                }
                animationWait = 0.0f; colliderWait = 0.0f;
                navigation.EndNav();
                EndAttackAnimation();
                StartHit();
                EndEffect();
                nextState = EnemyAIState.DAMAGE;
            }
            // 攻撃
            if (aiState != EnemyAIState.DAMAGE && aiState != EnemyAIState.ATTACK && GetAttack && endAttackAnimation && endDamageAnimation)
            {
                navigation.EndNav();   // Navigation
                attack.StartAttack();
                animationWait = 0.0f;   // これもAttack(アニメーションイベントでもいい)で管理
                StartAttack();  // Attack
                nextState = EnemyAIState.ATTACK;
            }
        }
        // 死亡
        if (hp.IsDead() && aiState != EnemyAIState.DEATH)
        {
            animator.SetTrigger("Death");
            navigation.EndNav();
            EndAttackAnimation();
            EndHit();
            attack.InvalidAllColliders();
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
                navigation.StartMoving();
                break;
            case EnemyAIState.CHASE:
                navigation.StartTracking();
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
                break;
        }
        if (Input.GetKeyDown(KeyCode.Return)) { }
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
        if (effect) attackEffect.SetActive(true);
    }

    public void EndAttackAnimation()
    {
        endAttackAnimation = true;
        nextState = EnemyAIState.WAIT;
    }

    public void ReturnWaitState()
    {
    }

    public void EndEffect()
    {
        if (effect) attackEffect.SetActive(false);
    }

    public void Attack()
    {
        GetAttack = true;
    }

    public void nAttack()
    {
        GetAttack = false;
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

    public void StartInvincible()
    {
        Debug.Log("無敵開始");
        invincible = true;
    }
    public void EndInvincible()
    {
        Debug.Log("無敵終了");
        invincible = false;
    }

    public void DestroyEnemy()
    {
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }

    public string GetEnemyName()
    {
        return enemyName;
    }
}
