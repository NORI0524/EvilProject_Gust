using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagetest : MonoBehaviour
{
    [SerializeField] int hp = 100;

    // ダメージ通知書
    public struct DamageArg
    {
        public int atkPower;    // 攻撃の威力
    }

    // ダメージ通知返信処理
    public struct DamageReply
    {
        public bool isGuard;           // 防御成功の可否
    }

    // ダメージ通知
    // dmg...攻撃者から送られるダメージ通知データ
    // rep...攻撃者へ返信する内容
    public bool isDamage(int dmg)
    {
        // HPを減らす
        //hp -= dmg.atkPower;
        hp -= dmg;

        // 防御しない
        //rep.isGuard = false;

        return true;
    }

    private void Update()
    {
        Debug.Log(hp);
    }

}
