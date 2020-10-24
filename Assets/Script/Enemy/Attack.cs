using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        //攻撃した相手がPlayerの場合
        if (other.CompareTag("Player"))
        {
            Debug.Log("攻撃がPlayerに当たりました！");
        }
    }
}
