﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HpState
{
    public static readonly uint None = 0;
    public static readonly uint Dead = 1 << 0;
    public static readonly uint Damage = 1 << 1;
}

/// <summary>
/// HPのコンポーネント
/// （耐久値のあるオブジェクト全てに付与させる）
/// </summary>
public class HpComponent : BaseStatusComponent
{
    [SerializeField,Tooltip("無敵の有効/無効")] private bool isNoDamage = false;
    [SerializeField, RangeDelta(0,5,1)] private float value = 0;
    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    public int Hp
    {
        get { return Value; }
        set { Value = value; }
    }

    public bool IsNoDamage
    {
        get { return isNoDamage; }
        set { isNoDamage = value; }
    }

    public void AddDamage(int damage)
    {
        if (isNoDamage) return;

        SubValue(damage);
        state.AddBit(HpState.Damage);
        if(Value <= 0)
        {
            state.AddBit(HpState.Dead);
        }
    }

    public void AddHeal(int heal)
    {
        AddValue(heal);
    }

    public bool IsDead()
    {
        return state.CheckBit(HpState.Dead);
    }

    public bool IsDamageTrigger()
    {
        var isDamage = state.CheckBit(HpState.Damage);

        if (isDamage)
        {
            state.FoldBit(HpState.Damage);
        }

        return isDamage;
    }

    public void Restart()
    {
        Value = maxValue;
        state.FoldALLBit();
    }
}
