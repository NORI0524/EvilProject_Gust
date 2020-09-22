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

    public void AddDamage(int damage)
    {
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
}
