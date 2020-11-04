using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中のキーマウのキーコンフィグ
/// </summary>
public enum GameKeyConfig
{
    None = KeyCode.None,

    //移動
    Forward = KeyCode.W,
    Back = KeyCode.S,
    Left = KeyCode.A,
    Right = KeyCode.D,

    //アクション
    Jump = KeyCode.Space,       //ジャンプ
    Avoid = KeyCode.LeftShift,  //回避
    Skill = KeyCode.Q,          //武器召喚
    Item = KeyCode.E,           //アイテム使用
    Event = KeyCode.F,          //イベントを発生させる時（宝箱やドアなど）

    //攻撃
    Attack_Light = KeyCode.Mouse0,
    Attack_Strong = KeyCode.Mouse1,
}

/// <summary>
/// デバッグ用のキーコンフィグ（追加されるかも）
/// </summary>
public enum DebugKeyConfig
{
    Restart = KeyCode.R,    //GameOver時にRでリスタート
}
