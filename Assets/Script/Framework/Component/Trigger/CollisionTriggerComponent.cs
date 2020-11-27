using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;


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
    [SerializeField]
    List<string> tagList = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (enterFunction.GetPersistentEventCount() == 0) return;
        if(tagList.Count == 0)
        {
            enterFunction.Invoke(other.gameObject);
        }
        else
        {
            foreach (var targetTag in tagList)
            {
                if (!other.gameObject.CompareTag(targetTag)) continue;
                enterFunction.Invoke(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (stayFunction.GetPersistentEventCount() == 0) return;

        if(tagList.Count == 0)
        {
            if (gameKey == GameKeyConfig.None)
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
        else
        {
            foreach (var targetTag in tagList)
            {
                if (gameKey == GameKeyConfig.None)
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (exitFunction.GetPersistentEventCount() == 0) return;

        if (tagList.Count == 0)
        {
            exitFunction.Invoke(other.gameObject);
        }
        else
        {
            foreach (var targetTag in tagList)
            {
                if (!other.gameObject.CompareTag(targetTag)) continue;
                exitFunction.Invoke(other.gameObject);
            }
        }
    }
}
