using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] int power = 1;          // 攻撃力
    [SerializeField] int criticalRate = 0;   // クリティカル率

    // ダメージ通知書
    private struct DamageArg
    {
        public int atkPower;
    }

    // ダメージ通知返信処理
    public struct DamageReply
    {
        public bool isGuard;           // 防御成功の可否
    }

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
        DamageReply rep;

        // 相手にダメージ情報を通知する
        //var you=other.GetComponent<damagetest>().isDamage(dmg,rep);
        

        // デバッグ
        Debug.Log(dmg.atkPower);
    }
}
