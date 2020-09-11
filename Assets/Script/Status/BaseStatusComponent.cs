using System.Collections;
using System.Collections.Generic;
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

    protected void AddValue(int add)
    {
        if (add <= 0) return;
        Value = Mathf.Min(Value + add, MaxValue);
    }

}
