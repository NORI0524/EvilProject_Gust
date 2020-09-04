using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WeaponManager : MonoBehaviour
{
    // 武器変更の関数
    // 
    public void WeaponChange()
    {
        // 現在の武器を削除
        // ゲームオブジェクトの取得
        GameObject currentWeapon = GameObject.Find("dark_sword");
        Destroy(currentWeapon);

        // プレハブのロード
        GameObject prf = (GameObject)Resources.Load("Prefabs/dark_sword");
        if(prf == null)
        {
            Debug.LogError("Prefabがロードされませんでした");
        }

        Vector3 weaponPos = GameObject.Find("dark_sword").transform.position;

        // プレハブをインスタンス化
        GameObject gameObject = (GameObject)Instantiate(prf, weaponPos, Quaternion.identity);
    }
}
