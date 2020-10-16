using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Events;


public class CollisionTriggerComponent : MonoBehaviour
{
    [Serializable] public class CallBackFunction : UnityEvent<GameObject> { }


    //当たり判定の開始
    [field: SerializeField] public CallBackFunction enterFunction { get; set; }

    //当たり判定中
    [field: SerializeField] public CallBackFunction stayFunction { get; set; }
    //当たり判定の終了
    [field: SerializeField] public CallBackFunction exitFunction { get; set; }

    [SerializeField] GameKeyConfig gameKey = GameKeyConfig.None;

    //タグを複数設定できるようにリスト
    [SerializeField] List<string> tagList = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        enterFunction.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if(gameKey == GameKeyConfig.None)
        {
            stayFunction.Invoke(other.gameObject);
        }
        else
        {
            if (gameKey.GetKeyDown())
            {
                stayFunction.Invoke(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        exitFunction.Invoke(other.gameObject);
    }
}
