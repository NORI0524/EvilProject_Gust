using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HpState
{
    public static readonly uint None = 0;
    public static readonly uint Dead = 1 << 0;
}

public class HpComponent : BaseStatusComponent
{
    [SerializeField,Tooltip("無敵の有効/無効")] private bool isNoDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        Value = maxValue;
        state.FoldALLBit();
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
}
