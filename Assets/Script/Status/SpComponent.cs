using UnityEngine;
using System.Collections;

public class SpState
{
    public static readonly uint None = 0;
    public static readonly uint Max = 1 << 0;
    public static readonly uint Ultimate = 1 << 1;
}

public class SpComponent : BaseStatusComponent
{
    [SerializeField] private float UltSeconds = 60;
    Timer ultTimer = null;

    // Start is called before the first frame update
    void Start()
    {
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

        //現在の残り時間からValueも連動して減少
        var rate = ultTimer.GetToSeconds() / ultTimer.GetToInitSeconds();
        Value = (int)(MaxValue * rate);

        //指定時間立ったら終了
        if(ultTimer.IsFinish())
        {
            Value = 0;
            state.FoldBit(SpState.Ultimate | SpState.Max);
            Debug.Log("Ultimate End !!");
        }
    }

    public void AddPoint(int point)
    {
        AddValue(point);
        if(IsMax())
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