using UnityEngine;
using UnityEditor;
using UnityScript.Steps;
using System.Runtime.CompilerServices;

public static class ExtensionVector3
{
    /// <summary>
    /// 指定の座標に移動
    /// </summary>
    /// <param name="target">指定の座標</param>
    /// <param name="distanceDelta">秒間に進む距離</param>
    public static void MoveTowars(this Vector3 vector,Vector3 target,float distanceDelta)
    {
        vector = Vector3.MoveTowards(vector, target, distanceDelta);
    }

    /// <summary>
    /// 指定の座標との中点を求める
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="target">指定の座標</param>
    /// <returns></returns>
    public static Vector3 Center(this Vector3 vector, Vector3 target)
    {
        return Vector3.Lerp(vector, target, 0.5f);
    }

    /// <summary>
    /// 指定の長さ以下のベクトルか判定
    /// </summary>
    /// <returns>指定以下：true、それ以外：false</returns>
    public static bool IsEnterDistance(this Vector3 vector,float distance)
    {
        return vector.sqrMagnitude <= distance.Pow2();
    }

    /// <summary>
    /// 文字列から Vector3 に変換させる
    /// </summary>
    /// <param name="str">変換したい Vector3 の文字列</param>
    /// <returns>変換された Vector3</returns>
    public static Vector3 Parse(this Vector3 vector, string str)
    {
        var elements = str.Trim('(', ')').Split(',');
        var vec = Vector3.zero;
        if (elements.Length <= 3)
        {
            vec.x = float.Parse(elements[0]);
            vec.y = float.Parse(elements[1]);
            vec.z = float.Parse(elements[2]);
        }
        return vec;
    }
}

public static class ExtensionFloat
{
    public static float Pow2(this float value)
    {
        return Mathf.Pow(value, 2);
    }
}