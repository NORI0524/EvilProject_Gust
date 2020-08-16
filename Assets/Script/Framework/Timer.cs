using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カウントダウン用タイマークラス
public class Timer
{
    private float totalSeconds;
    private int minute;
    private float seconds;

    private readonly float totalInitSeconds;
    private readonly int initMinute;
    private readonly float initSeconds;

    BitFlag flag = new BitFlag();


    class TimerState
    {
        public const uint None = 0;
        public const uint Start = 1 << 0;
        public const uint Finish = 1 << 1;
        public const uint Loop = 1 << 2;
        public const uint Pause = 1 << 3;

        public const uint Exception = Start | Finish;
    }

    public Timer(float _sec)
    {
        _sec = Mathf.Clamp(_sec, 0.0f, 3600.0f);
        totalSeconds = totalInitSeconds = _sec;
        flag.FoldALLBit();
    }

    public Timer(int _sec)
    {
        _sec = Mathf.Clamp(_sec, 0, 3600);
        minute = initMinute = _sec / 60;
        seconds = initSeconds = _sec % 60;
        totalSeconds = totalInitSeconds = seconds;
        flag.FoldALLBit();
    }
    //59:59まで指定可能
    public Timer(int _min, int _sec)
    {
        minute = initMinute = Mathf.Clamp(_min, 0, 59);
        seconds = initSeconds = Mathf.Clamp(_sec, 0, 59);
        totalSeconds = totalInitSeconds = minute * 60 + seconds;
        flag.FoldALLBit();
    }

    public Timer(int _min, float _sec)
    {
        minute = initMinute = Mathf.Clamp(_min, 0, 59);
        seconds = initSeconds = Mathf.Clamp(_sec, 0.0f, 59.0f);
        totalSeconds = totalInitSeconds = minute * 60 + seconds;
        flag.FoldALLBit();
    }

    public void Update()
    {
        if (flag.CheckBit(TimerState.Exception)) flag.FoldBit(TimerState.Finish);

        if (!flag.CheckBit(TimerState.Start)) return;
        if (flag.CheckBit(TimerState.Pause))   return;

        //１フレームにかかった時間を減算
        totalSeconds -= Time.deltaTime;
        minute = (int)totalSeconds / 60;
        seconds = totalSeconds % 60.0f;

        //タイマー終了
        if(totalSeconds <= 0.0f)
        {
            flag.FoldBit(TimerState.Start);
            flag.AddBit(TimerState.Finish);
        }

        if (!flag.CheckBit(TimerState.Finish)) return;

        //タイマーをループさせる場合
        if (flag.CheckBit(TimerState.Loop))
        {
            seconds = initSeconds;
            minute = initMinute;
            totalSeconds = totalInitSeconds;
            flag.AddBit(TimerState.Start);
        }
    }

    public void Start() { flag.AddBit(TimerState.Start); }
    public void Stop() { flag.AddBit(TimerState.Pause); }
    public void ReStart() { flag.FoldBit(TimerState.Pause); }
    public void EnabledLoop(){ flag.AddBit(TimerState.Loop); }

    public bool IsFinish() { return flag.CheckBit(TimerState.Finish); }
    public bool IsStart() { return flag.CheckBit(TimerState.Start); }
    public bool IsPause() { return flag.CheckBit(TimerState.Pause); }

    public float GetToSeconds() { return totalSeconds; }
    public float GetSeconds() { return seconds; }
    public int GetMinute() { return minute; }

    public float GetInitSeconds() { return initSeconds; }
    public float GetToInitSeconds() { return totalInitSeconds; }
    public int GetInitMinute() { return initMinute; }
}
