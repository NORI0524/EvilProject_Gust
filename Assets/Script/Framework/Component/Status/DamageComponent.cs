using UnityEngine;
using System.Collections;

public class DamageComponent : BaseStatusComponent
{

    [SerializeField] private int damageValue = 10;
    [SerializeField, Range(0.0f, 1.0f)] private float criticalRate = 0.0f;

    // Use this for initialization
    void Start()
    {
        currentValue = damageValue;
    }

    public void ChangeDamageHalf()
    {
        MultiplyValue(0.5f);
    }
    public void ChangeDamageDouble()
    {
        MultiplyValue(2.0f);
    }
}