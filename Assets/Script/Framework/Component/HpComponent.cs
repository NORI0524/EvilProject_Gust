using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HpState
{
    public static readonly uint None = 0;
    public static readonly uint Dead = 1 << 0;
}

public class HpComponent : MonoBehaviour
{
    [SerializeField] private int hp;

    private int initHp = 0;

    BitFlag state = new BitFlag();

    // Start is called before the first frame update
    void Start()
    {
        initHp = hp;
        state.FoldALLBit();
    }

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public void AddDamage(int damage)
    {
        if (damage <= 0) return;
        hp = Mathf.Max(hp - damage, 0);
    }

    public void AddHeal(int heal)
    {
        if (heal <= 0) return;
        hp = Mathf.Min(hp + heal, initHp);
    }
}
