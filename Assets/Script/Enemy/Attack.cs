using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool backStepFlg = false;
    private Rigidbody rigidbody;
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
        //explosionEffect.enabled = true;
        explosionEffect.CreateBarrage();
    }
    public void EndExplosionEffect()
    {
        //var explosionEffect = GetComponent<BarrageComponent>();
        //explosionEffect.enabled = false;
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

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(60f);
    }
}
