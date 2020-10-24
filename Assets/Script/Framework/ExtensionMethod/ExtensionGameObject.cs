using UnityEngine;
using System.Collections;

public static class ExtensionGameObject
{
    public static T GetorAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        if(component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }


    /// <summary>
    /// 自身のオブジェクト破棄
    /// </summary>
    public static void Destroy(this GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }

    /// <summary>
    /// 指定したオブジェクトを親に設定する（子になる）
    /// </summary>
    /// <param name="parent">親にするオブジェクト</param>
    public static void SetParent(this GameObject gameObject, GameObject parent)
    {
        gameObject.transform.parent = parent.transform;
        gameObject.transform.LocalInitialize();
    }
    /// <summary>
    /// 文字列で指定したオブジェクトを親に設定する（子になる）
    /// </summary>
    public static void SetParent(this GameObject gameObject, string _parentName)
    {
        var parentObj = GameObject.Find(_parentName);
        if (parentObj == null) return;
        gameObject.SetParent(parentObj);
    }

    /// <summary>
    /// 親との関係を解除
    /// </summary>
    public static void RemoveParent(this GameObject gameObject)
    {
        gameObject.transform.parent = null;
    }

    /// <summary>
    /// 指定したオブジェクトを子として追加する
    /// </summary>
    /// <param name="child">子として追加するオブジェクト</param>
    public static void AddChild(this GameObject gameObject, GameObject child)
    {
        child.SetParent(gameObject);
    }

    /// <summary>
    /// 文字列で指定したオブジェクトを子として追加する
    /// </summary>
    /// <param name="child">子として追加するオブジェクト</param>
    public static void AddChild(this GameObject gameObject, string _childName)
    {
        var childObj = GameObject.Find(_childName);
        if (childObj == null) return;
        childObj.SetParent(gameObject);
    }

    /// <summary>
    /// 複数のタグの内、どれか１つ存在するかチェック
    /// </summary>
    /// <param name="tagList"></param>
    /// <returns>存在：true、無し : false</returns>
    public static bool CompareTag(this GameObject gameObject, params string[] tagList)
    {
        bool isFound = false;

        foreach (var tag in tagList)
        {
            if (gameObject.CompareTag(tag))
            {
                isFound = true;
                break;
            }
        }
        return isFound;
    }
}