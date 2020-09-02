using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
  ここのやつ
 https://qiita.com/okuhiiro/items/3d69c602b8538c04a479


    こいつの役割
    ・MonoBehaviour を継承した状態でシングルトンにできる
    ・ObjectOnLoad を有効にしたら多分シーンまたいでも存在できると思う（試してない）

*/

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    // シーンまたぐかの有効フラグ
    [SerializeField] bool ObjectOnLoad = false;

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    virtual protected void Awake()
    {
        // 他のGameObjectにアタッチされているか調べる.
        // アタッチされている場合は破棄する.
        if (this != Instance)
        {
            Destroy(this);
            //Destroy(this.gameObject);
            Debug.LogError(
                typeof(T) +
                " は既に他のGameObjectにアタッチされているため、コンポーネントを破棄しました." +
                " アタッチされているGameObjectは " + Instance.gameObject.name + " です.");
            return;
        }

        // シーンをまたいで存在するかしないかの判定
        if (ObjectOnLoad == true)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
