using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BaseStatusComponent : MonoBehaviour
{
    protected int currentValue = 0;
    [SerializeField] protected int maxValue = 100;
    [SerializeField] protected int minValue = 0;

    protected BitFlag state = new BitFlag();

    public int Value
    {
        get { return currentValue; }
        set { currentValue = value; }
    }

    public int MinValue
    {
        get { return minValue; }
    }
    public int MaxValue
    {
        get { return maxValue; }
    }

    public void AddValue(int add)
    {
        if (add <= 0) return;
        Value = Mathf.Min(Value + add, MaxValue);
    }

    public void SubValue(int sub)
    {
        if (sub <= 0) return;
        Value = Mathf.Max(Value - sub, minValue);
    }

    public void MultiplyValue(float rate = 1.0f)
    {
        Value = Mathf.RoundToInt(Value * rate);
    }

    public bool IsMax()
    {
        return Value >= MaxValue;
    }

    public bool IsMin()
    {
        return Value <= MinValue;
    }

}
