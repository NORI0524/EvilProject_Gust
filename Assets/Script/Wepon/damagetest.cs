using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagetest : testBase
{
    HpComponent hp= new HpComponent();

    // HP
    [SerializeField] int HP = 100;

    private void Start()
    {
        // Hpの設定
        hp.Hp = HP;
    }

    // ダメージ通知
    // dmg...攻撃者から送られるダメージ通知データ
    // rep...攻撃者へ返信する内容
    public bool isDamage(DamageArg dmg, DamageReply rep)
    {
        // HPを減らす
        hp.AddDamage(dmg.atkPower);

        // 防御しない
        rep.isGuard = false;

        return true;
    }

    private void Update()
    {
       // Debug.Log(hp.Hp);
    }

}
