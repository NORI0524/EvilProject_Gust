using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singletonテンプレートクラス
public class Singleton<T> where T : class, new()
{
    private static T obj = null;

    Singleton() { }

    public static T Instance
    {
        get
        {
            if (obj == null) obj = new T();
            return obj;
        }
    }
}
