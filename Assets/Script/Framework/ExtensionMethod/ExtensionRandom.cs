using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class ExtensionRandom
{
    public static Vector3 InsideCicleRange(this Random self, float _distance = 1.0f)
    {
        var point = Random.insideUnitCircle;
        Vector3 pos = new Vector3(point.x, 0.0f, point.y);
        return pos * _distance;
    }

    public static Vector3 OnCicleRange(this Random self, float _distance = 1.0f)
    {
        var radian =  Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(radian), 0.0f, Mathf.Sin(radian));
        return pos * _distance;
    }

    public static bool IsInsidePercent(this Random self, float _target)
    {
        var percent = Mathf.Clamp(_target, 0.0f, 100.0f);
        return Random.Range(0.0f, 100.0f) <= percent;
    }
}
