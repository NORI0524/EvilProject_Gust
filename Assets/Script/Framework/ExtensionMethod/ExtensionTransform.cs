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

    /// <summary>
    /// 指定のTransformをセット（Transformのsetterが無かった...）
    /// </summary>
    /// <param name="target"></param>
    public static void Set(this Transform transform, Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        transform.localPosition = target.localPosition;
        transform.localRotation = target.localRotation;
        transform.localScale = target.localScale;
        transform.parent = target.parent;
    }
}