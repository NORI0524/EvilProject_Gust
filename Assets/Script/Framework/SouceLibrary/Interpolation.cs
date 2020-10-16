using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//補間クラス
public class Interpolation
{
    //二次ベジェ曲線
    public static Vector3 BezierCurve(Vector3 _start, Vector3 _middle, Vector3 _end, float _frame)
    {
        Vector3 first = Vector3.Lerp(_start, _middle, _frame);
        Vector3 second = Vector3.Lerp(_middle, _end, _frame);
        return Vector3.Lerp(first, second, _frame);
    }
    //三次ベジェ曲線
    public static Vector3 BezierCurve(Vector3 _start, Vector3 _middle1, Vector3 _middle2, Vector3 _end, float _frame)
    {
        Vector3 first = BezierCurve(_start, _middle1, _middle2, _frame);
        Vector3 second = BezierCurve(_middle1, _middle2, _end, _frame);
        return Vector3.Lerp(first, second, _frame);
    }

    //色の二次ベジェ曲線
    public static Color BezierCurve(Color _start,Color _middle,Color _end,float _frame)
    {
        Color first = Color.Lerp(_start, _middle, _frame);
        Color seconds = Color.Lerp(_middle, _end, _frame);
        return Color.Lerp(first, seconds, _frame);
    }
    //色の三次ベジェ曲線
    public static Color BezierCurve(Color _start, Color _middle1, Color _middle2, Color _end, float _frame)
    {
        Color first = BezierCurve(_start, _middle1, _middle2, _frame);
        Color seconds = BezierCurve(_middle1, _middle2, _end, _frame);
        return Color.Lerp(first, seconds, _frame);
    }
}
