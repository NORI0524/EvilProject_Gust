using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 
    処理内容：Time の値が0になったら消す
              待機モーションのフラグが取れれば完成かな？
     
 */


public class Weapondelete : MonoBehaviour
{

    bool TimeFlg = false;

    // ↓0になったら消す用
    [SerializeField] int Time = 100;

    // 初期値の保留
    int DefaultTime = 0;

    private void Start()
    {
        DefaultTime = Time;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            TimeFlg = true;
        }

        if(TimeFlg == true)
        {
            Time--;
            if (Time < 0)
            {
                TimeFlg = false;
                Time = DefaultTime;
                foreach (Transform item in this.gameObject.transform)
                {
                    item.gameObject.SetActive(false);
                }
                Debug.Log("消えた");
            }
        }


        // デバッグ用
        {
            if (Input.GetKey(KeyCode.C))
            {
                foreach (Transform item in this.gameObject.transform)
                {
                    item.gameObject.SetActive(true);
                }
            }
            Debug.Log(TimeFlg);
        }

    }
}
