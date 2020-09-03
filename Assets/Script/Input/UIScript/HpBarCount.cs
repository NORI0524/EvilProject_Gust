using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpBarCount : MonoBehaviour
{
    Slider slider;
    float hp;

    // HpBerが0になった時の通知用？(いるかわからんけど入れた)
    bool flag;

    public float GetHpBer() { return hp; }
    public void SetHpBer(float h) { hp = h; }

    void Start()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        hp = slider.maxValue;  // HpBerの最大値
        flag = false;
    }

    void Update()
    {
        // デバッグ用
        {
            if (Input.GetKey(KeyCode.Space))
            {
                hp -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                hp = slider.maxValue;
            }
            Debug.Log(hp);
        }


        if (hp < 0)
        {
            hp = 0;
            flag = true;
            Debug.Log("HpBerは0じゃい");
        }
        slider.value = hp;
    }
}
