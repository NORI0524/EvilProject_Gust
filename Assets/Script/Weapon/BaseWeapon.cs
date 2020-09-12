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

    WeaponSummonSystem weaponSummonSys = null;

    private void Start()
    {
        weaponSummonSys = GameObject.Find("unitychan_dynamic").GetComponent<WeaponSummonSystem>();
    }


    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        //if (isAttack == false)
        //{ return; }

        //-------------------
        // ダメージ処理
        //-------------------

        // 相手にダメージ情報を通知する
        var target = other.GetComponent<HpComponent>();
        if (target == null) return;

        target.AddDamage(power);

        //-------------------
        // 攻撃した時にSpを増加
        //-------------------
        if(weaponSummonSys.IsSummon() == false)
        {
            weaponSummonSys.AddSp();
        }

        // デバッグ
        Debug.Log("武器の攻撃力：" + power);
        Debug.Log("敵のHP:" + target.Hp);

    }
}
