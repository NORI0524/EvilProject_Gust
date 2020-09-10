using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] int power = 1;          // 攻撃力
    [SerializeField] int criticalRate = 0;   // クリティカル率

    // 改修の必要性アリ==========
    [SerializeField] bool isAttack = false; // 攻撃状態か否か

    // 攻撃状態のセッター
    public void SetIsAttack(bool isattack)
    {
        isAttack = isattack;
    }
    //===========================


    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if (isAttack == false)
        { return; }

        //-------------------
        // ダメージ処理
        //-------------------

        // 相手にダメージ情報を通知する
        other.GetComponent<HpComponent>().AddDamage(power);

        // デバッグ
        Debug.Log(power);

    }
}
