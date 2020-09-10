using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBase : MonoBehaviour
{
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
}
