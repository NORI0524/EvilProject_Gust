using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject Enemy;

    [SerializeField] Rigidbody rigidbody;
    [SerializeField] Animator animator;
    [SerializeField] Navigation nav;

    [SerializeField] private int attackValue = 1;
    private int attackCount = 0;        // 攻撃種類の番号

    private bool backStepFlg = false;   // バックステップをしているかどうか
    private bool jump = false;

    public GameObject Rock;
    [SerializeField] GameObject circle;
    private Transform target;
    

    private void Start()
    {
        backStepFlg = false;
        jump = false;
    }

    private void Update()
    {
        if (backStepFlg)
        {
            rigidbody.AddForce(-transform.forward * 0.13f, ForceMode.VelocityChange);
        }
        if (jump)
        {
            rigidbody.AddForce(transform.forward * 0.7f, ForceMode.VelocityChange);
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

    public void ValidColliders(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            var cols = obj.GetComponents<Collider>();
            foreach (var col in cols)
            {
                col.enabled = true;
            }
        }
    }
    public void ValidCollider(string tag)
    {
        Collider attackCollider = GameObject.FindGameObjectWithTag(tag).GetComponent<Collider>();
        attackCollider.enabled = true;
    }
    public void InvalidColliders(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            var cols = obj.GetComponents<Collider>();
            foreach (var col in cols)
            {
                col.enabled = false;
            }
        }
    }
    public void InvalidCollider(string tag)
    {
        Collider attackCollider;
        attackCollider = GameObject.FindGameObjectWithTag(tag).GetComponent<Collider>();
        attackCollider.enabled = false;
    }
    public void InvalidAllColliders()
    {
        Collider[] attackColliders = transform.GetComponents<Collider>();
        foreach (var col in attackColliders) { col.enabled = false; }
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
        var explosionEffect = GameObject.Find("ExplosionEffect").GetComponent<BarrageComponent>();
        StartCoroutine(explosionEffect.CreateBarrage());
    }
    public void Explosion(string tag)
    {
        var objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (var obj in objects)
        {
            var cols = obj.GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                col.enabled = true;
            }
        }
        Invoke("EndExplosion", 0.1f);
    }
    public void EndExplosion()
    {
        var explosions = GameObject.FindGameObjectsWithTag("Attack2");
        foreach (var explosion in explosions)
        {
            var cols = explosion.GetComponentsInChildren<Collider>();
            foreach (var col in cols)
            {
                col.enabled = false;
            }
        }
    }

    public void StartRock()
    {
        target = nav.Player;
        circle.transform.position = new Vector3(target.position.x, -56.5f, target.position.z);
        circle.SetActive(true);
        Invoke("RockSmash", 1.0f);
    }
    public void RockSmash()
    {
        circle.SetActive(false);
        Rock = (GameObject)Resources.Load("Prefabs/Enemy/Crystalsv02");
        GameObject obj = (GameObject)Instantiate(Rock, new Vector3(circle.transform.position.x, -60f, circle.transform.position.z), Quaternion.identity);
    }

    public void StartJumping()
    {
        Debug.Log("jump開始");
        rigidbody.SetFreezePositionX(false);
        rigidbody.SetFreezePositionY(false);
        rigidbody.SetFreezePositionZ(false);
        jump = true;
    }
    public void EndJumping()
    {
        Debug.Log("jump終わり");
        jump = false;
        rigidbody.SetFreezePositionX(true);
        rigidbody.SetFreezePositionY(true);
        rigidbody.SetFreezePositionZ(true);
    }
}
