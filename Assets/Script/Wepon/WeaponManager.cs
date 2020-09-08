using RPGCharacterAnims;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// 武器の種類
public enum WeaponType
{
    DARK_SWORD,
    AXE,
    HAMMER,
    MACE,
    SKULLAXE
}

public class WeaponManager : MonoBehaviour
{
    // 武器のオブジェクト
    GameObject darkswordObj;
    GameObject axeObj;
    GameObject hammerObj;
    GameObject maceObj;
    GameObject skullaxeObj;

    // 現在持っている武器
    GameObject currentWeapon;

    private void Start()
    {
        //------------------------------------
        // 使用する武器の一括ロード
        //------------------------------------
        // dark_wordのロード
        GameObject dark_sword = (GameObject)Resources.Load("Prefabs/dark_sword");
        if (dark_sword == null)
        {
            Debug.LogError("dark_swordがロードされませんでした");
        }
        // axeのロード
        GameObject axe = (GameObject)Resources.Load("Prefabs/axe");
        if (axe == null)
        {
            Debug.LogError("axeがロードされませんでした");
        }
        // hammerのロード
        GameObject hammer = (GameObject)Resources.Load("Prefabs/hammer");
        if (axe == null)
        {
            Debug.LogError("hammerがロードされませんでした");
        }
        // maceのロード
        GameObject mace = (GameObject)Resources.Load("Prefabs/mace");
        if (axe == null)
        {
            Debug.LogError("maceがロードされませんでした");
        }
        // skull_axeのロード
        GameObject skull_axe = (GameObject)Resources.Load("Prefabs/skull_axe");
        if (axe == null)
        {
            Debug.LogError("skull_axeがロードされませんでした");
        }

        //------------------------------------
        // 使用する武器のインスタンス化
        //------------------------------------
        {
            // dark_swordをインスタンス化
            darkswordObj = (GameObject)Instantiate(dark_sword, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // 無効化
            darkswordObj.SetActive(false);

            // axeをインスタンス化
            axeObj = (GameObject)Instantiate(axe, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // 無効化
            axeObj.SetActive(false);

            // hammerをインスタンス化
            hammerObj = (GameObject)Instantiate(hammer, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // 無効化
            hammerObj.SetActive(false);

            // maceをインスタンス化
            maceObj = (GameObject)Instantiate(mace, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // 無効化
            maceObj.SetActive(false);

            // skull_axeをインスタンス化
            skullaxeObj = (GameObject)Instantiate(skull_axe, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            // 無効化
            skullaxeObj.SetActive(false);
        }

        // 最初持っている武器の設定
        currentWeapon = darkswordObj;
        // 仮のポジション
        Vector3 initPos = new Vector3(-10.0f, 22.0f, -5.5f);
        currentWeapon.transform.position = initPos;
        // 武器を有効化
        currentWeapon.SetActive(true);
    }

    // 武器変更の関数
    // weapontype...WeaponType型
    public void ChangeWeapon(WeaponType weapontype)
    {
        // 元のオブジェクトの位置を記憶しておく
        Vector3 weaponPos = currentWeapon.transform.position;
        // 武器を無効化する
        currentWeapon.SetActive(false);

        // 仮
        // 
        if (weapontype == WeaponType.AXE)
        {
            currentWeapon = axeObj;
        }
        else if(weapontype == WeaponType.DARK_SWORD)
        {
            currentWeapon = darkswordObj;
        }
        else if (weapontype == WeaponType.HAMMER)
        {
            currentWeapon = hammerObj;
        }
        else if (weapontype == WeaponType.MACE)
        {
            currentWeapon = maceObj;
        }
        else if (weapontype == WeaponType.SKULLAXE)
        {
            currentWeapon = skullaxeObj;
        }

        // 新しい武器の位置を変更する
        axeObj.transform.position = weaponPos;
        // 新しい武器を有効化する
        axeObj.SetActive(true);
    }
}
