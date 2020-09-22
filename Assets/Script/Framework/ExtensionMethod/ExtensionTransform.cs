using UnityEngine;
using System.Collections;
using UnityEditor.PackageManager.Requests;
using System.Runtime.CompilerServices;

public static class ExtensionTransform
{
    /// <summary>
    /// ローカルのTransformを初期化
    /// </summary>
    /// <param name="transform"></param>
    public static void LocalInitialize(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}