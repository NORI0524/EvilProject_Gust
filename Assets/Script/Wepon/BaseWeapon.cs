using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : testBase
{
    [SerializeField] int power = 1;          // 攻撃力
    [SerializeField] int criticalRate = 0;   // クリティカル率

    // 当たり判定
    private void OnTriggerEnter(Collider other)
    {
        //-------------------
        // ダメージ処理
        //-------------------
        // 通知用データ
        DamageArg dmg = new DamageArg();
        dmg.atkPower = power;
        // 返信用データ
        DamageReply rep = new DamageReply();

        // 相手にダメージ情報を通知する
        var you=other.GetComponent<damagetest>().isDamage(dmg,rep);
        
        // デバッグ
        Debug.Log(dmg.atkPower);
    }
}
