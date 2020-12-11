using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combocount : MonoBehaviour
{
    ImageNo _spriteRendererNo;
    Color color;

    // こいつが数値変更用
    private int Count_Combo = 0;
    [Header("仕様 :color.a が0になるとSetActiveを false にしてるよ ")]
    [Header("そして、コンボ数も0にリセットしてるよ")]

    [Header("使い方 : ComboCount()にコンボの数入れれば増えるよ")]
    [Header("コンボが切れたらCollarReset()呼んでね(色をもとに戻してSetActiveをtrueにしてるよ)")]

    [Header("数字の幅の間隔")]
    [SerializeField]
    int width = 200;
    [Header("数字の大きさ調整")]
    [SerializeField]
    Vector3 vector;

    [Header("透明にする速さ")]
    [SerializeField]
    float speed = 0.001f;

    [Header("デバッグ用(Qキーで強制的にコンボ加算)")]
    [SerializeField]
    public bool debugflg = false;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRendererNo = GetComponent<ImageNo>();

        CollarReset();

        vector.x = vector.y = vector.z = 0.1f;
        _spriteRendererNo.transform.localScale = vector;
    }
    // Update is called once per frame
    void Update()
    {
        _spriteRendererNo.ChangeSpan(width);

        // デバッグ用
        if (debugflg == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CollarReset();
                Count_Combo++;
            }
        }

        color.a -= speed;

        if (color.a <= 0f)
        {
            Count_Combo = 0;
            gameObject.SetActive(false);
        }

        _spriteRendererNo.ChangeColor(color);
        _spriteRendererNo.SetNo(Count_Combo);
    }

    void CollarReset()
    {
        color.r = color.g = color.b = color.a = 1f;

        gameObject.SetActive(true);
    }

    // コンボ数を受け取る用
    void ComboCount(int cnt)
    {
        Count_Combo += cnt;
    }


}
