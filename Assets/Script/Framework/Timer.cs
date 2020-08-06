using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カウントダウン用タイマークラス
public class Timer
{
    private int minute;
    private int seconds;

    private int initMinute;
    private int initSeconds;

    private float CountSeconds;

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

    public Timer(int _sec)
    {
        _sec = Mathf.Clamp(_sec, 0, 3600);
        minute = initMinute = _sec / 60;
        seconds = initSeconds = _sec % 60;
        flag.FoldALLBit();
    }
    //59:59まで指定可能
    public Timer(int _min, int _sec)
    {
        minute = initMinute = Mathf.Clamp(_min, 0, 59);
        seconds = initSeconds = Mathf.Clamp(_sec, 0, 59);
        flag.FoldALLBit();
    }

    public void Update()
    {
        if (flag.CheckBit(TimerState.Exception)) flag.FoldBit(TimerState.Finish);

        if (!flag.CheckBit(TimerState.Start)) return;
        if (flag.CheckBit(TimerState.Pause))   return;

        //１フレームにかかった時間を加算
        CountSeconds += Time.deltaTime;
        if(CountSeconds >= 1.0f)
        {
            CountSeconds = 0.0f;
            //カウントダウン処理
            if (--seconds < 0)
            {
                seconds = minute > 0 ? 59 : 0;
                minute = Mathf.Max(minute - 1, 0);
            }

            //タイマー終了
            if (minute == 0 && seconds == 0)
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
                flag.AddBit(TimerState.Start);
            }
        }
    }

    public void Start() { flag.AddBit(TimerState.Start); }
    public void Stop() { flag.AddBit(TimerState.Pause); }
    public void ReStart() { flag.FoldBit(TimerState.Pause); }
    public void EnabledLoop(){ flag.AddBit(TimerState.Loop); }

    public bool IsFinish() { return flag.CheckBit(TimerState.Finish); }
    public bool IsStart() { return flag.CheckBit(TimerState.Start); }
    public bool IsPause() { return flag.CheckBit(TimerState.Pause); }

    public int GetSeconds() { return seconds; }
    public int GetToSeconds() { return seconds + minute * 60; }
    public int GetMinute() { return minute; }

    public int GetInitSeconds() { return initSeconds; }
    public int GetToInitSeconds() { return initSeconds + initMinute * 60; }
    public int GetInitMinute() { return initMinute; }
}
