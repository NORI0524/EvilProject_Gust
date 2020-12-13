using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Animator animator;

    [SerializeField] private int attackValue = 1;
    private int attackCount = 0;        // 攻撃種類の番号

    private bool backStepFlg = false;   // バックステップをしているかどうか

    private void Start()
    {
        backStepFlg = false;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (backStepFlg)
        {
            rigidbody.AddForce(-transform.forward * 0.07f, ForceMode.VelocityChange);
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }

    public void StartAttack()
    {
        attackCount++;
        if (attackCount > attackValue - 1) { attackCount -= attackValue; }
        switch (attackCount)
        {
            case 0:
                animator.SetTrigger("Attack");
                break;
            case 1:
                animator.SetTrigger("Attack2");
                break;
            case 2:
                animator.SetTrigger("Attack3");
                break;
        }
    }
    public void ValidCollider(string tag)
    {
        Collider attackCollider;
        attackCollider = GameObject.FindGameObjectWithTag(tag).GetComponent<Collider>();
        attackCollider.enabled = true;
    }
    public void InvalidCollider()
    {
        Collider[] attackColliders = transform.GetComponents<Collider>();
        foreach (var col in attackColliders) { col.enabled = false; }
    }
    public void InvalidCollider(string tag)
    {
        Collider attackCollider;
        attackCollider = GameObject.FindGameObjectWithTag(tag).GetComponent<Collider>();
        attackCollider.enabled = false;
    }

    public void StartBackStep()
    {
        rigidbody.SetFreezePositionX(false);
        rigidbody.SetFreezePositionZ(false);
        backStepFlg = true;
    }
    public void EndBackStep()
    {
        backStepFlg = false;
        rigidbody.SetFreezePositionX(true);
        rigidbody.SetFreezePositionZ(true);
    }

    public void ExplosionEffect()
    {
        var explosionEffect = GetComponentInChildren<BarrageComponent>();
        explosionEffect.CreateBarrage();
    }
    public void Explosion()
    {
        var explosions = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (var explosion in explosions)
        {
            var cols = explosion.GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                col.enabled = true;
            }
        }
        Invoke("EndExplosion", 0.1f);
    }
    public void EndExplosion()
    {
        var explosions = GameObject.FindGameObjectsWithTag("Explosion");
        foreach (var explosion in explosions)
        {
            var cols = explosion.GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                col.enabled = false;
            }
        }
    }

}
