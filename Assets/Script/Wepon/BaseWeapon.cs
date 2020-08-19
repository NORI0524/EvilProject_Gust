using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField]
    float damage = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(damage);
    }
}
