using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 
 */
public class Hit : MonoBehaviour
{
    [Header("エフェクト")]
    [SerializeField] private GameObject damagePrefab = null;
    HitStopSlowAnim slowAnim;
    void Start()
    {
       slowAnim = new HitStopSlowAnim();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (damagePrefab != null)
        {
            Instantiate(damagePrefab, other.transform.position, Quaternion.identity);
        }

        slowAnim = other.GetComponent<HitStopSlowAnim>();
        if (slowAnim.IsSlowDown() == false)
        {
            slowAnim.SlowDown();
        }
    }
}
