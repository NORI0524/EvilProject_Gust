using UnityEngine;
using System.Collections;

public class SpState
{
    public static readonly uint None = 0;
    public static readonly uint Max = 1 << 0;
    public static readonly uint Ultimate = 1 << 1;
}

public class SpComponent : MonoBehaviour
{
    [SerializeField] private int MaxSp = 100;
    [SerializeField] private float UltSeconds = 60;
    private int sp = 0;

    BitFlag state = new BitFlag();
    Timer ultTimer = null;

    // Start is called before the first frame update
    void Start()
    {
        sp = 0;
        state.FoldALLBit();
        ultTimer = new Timer(UltSeconds);
        ultTimer.EnabledLoop();
        ultTimer.Start();
    }

    void Update()
    {
        if (state.CheckBit(SpState.Ultimate) == false) return;

        //アルティメット中
        ultTimer.Update();

        //現在の残り時間からspも連動して減少
        var rate = ultTimer.GetToSeconds() / ultTimer.GetToInitSeconds();
        sp = (int)(MaxSp * rate);

        //指定時間立ったら終了
        if(ultTimer.IsFinish())
        {
            sp = 0;
            state.FoldBit(SpState.Ultimate);
            Debug.Log("Ultimate End !!");
        }
    }

    public int Sp
    {
        get { return sp; }
        set { sp = value; }
    }

    public void AddPoint(int point)
    {
        if (point <= 0) return;
        sp = Mathf.Min(sp + point, MaxSp);

        if(sp == MaxSp)
        {
            state.AddBit(SpState.Max);
        }
    }

    public void TriggerUltimate()
    {
        if(state.CheckBit(SpState.Max))
        {
            state.AddBit(SpState.Ultimate);
        }
    }

    public bool IsUltimate()
    {
        return state.CheckBit(SpState.Ultimate);
    }
}