//using RPGCharacterAnims;
using RPGCharacterAnims;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

// 武器の種類
public enum WeaponType
{
    None,
    DARK_SWORD,
    AXE,
    HAMMER,
    MACE,
    SKULLAXE
}

public class WeaponManager : MonoBehaviour
{
    //指定の親オブジェクト（追従させるため）
    [SerializeField] private GameObject parentObject = null;

    //武器リスト
    [SerializeField] private List<GameObject> weaponList = null;

    //武器管理リスト
    private Dictionary<WeaponType, GameObject> weaponDict = null;

    // 現在持っている武器
    private GameObject currentWeapon = null;

    //現在持っている武器のタイプ
    private WeaponType currentWeaponType = WeaponType.None;

    // ディゾルブ用のフラグ
    bool isDissolve = false;
    private Renderer rend;
    [SerializeField] float DissolveSpeed = 1.0f;

    private void Start()
    {
        //親オブジェクトが無ければ
        if (parentObject == null) return;

        //武器リストが無ければ
        if (weaponList == null) return;

        //武器管理リストを生成
        weaponDict = new Dictionary<WeaponType, GameObject>();

        //------------------------------------
        // 使用する武器をインスタンス化し登録
        //------------------------------------
        foreach (var weaponPref in weaponList)
        {
            if (weaponPref == null)
            {
                Debug.LogError(weaponPref.name + "がロードされませんでした");
                continue;
            }

            WeaponType type = WeaponType.None;

            switch (weaponPref.name)
            {
                case "dark_sword":
                    type = WeaponType.DARK_SWORD;
                    break;

                case "axe":
                    type = WeaponType.AXE;
                    break;

                case "hammer":
                    type = WeaponType.HAMMER;
                    break;

                case "mace":
                    type = WeaponType.MACE;
                    break;

                case "skull_axe":
                    type = WeaponType.SKULLAXE;
                    break;

                default:
                    break;
            }
            if (type != WeaponType.None)
            {
                //既にあれば
                if (weaponDict.ContainsKey(type)) continue;

                var weaponObj = GameObject.Instantiate(weaponPref);
                weaponObj.SetActive(false);
                weaponDict.Add(type, weaponObj);
            }
        }

        //初期状態
        currentWeapon = weaponDict[WeaponType.DARK_SWORD];
        currentWeapon.SetActive(false);
        currentWeaponType = WeaponType.None;

    }

    // 武器変更の関数
    // weapontype...WeaponType型
    public IEnumerator ChangeWeapon(WeaponType weapontype)
    {
        if (currentWeapon == null) yield return new WaitForEndOfFrame();

        if(currentWeaponType != WeaponType.None)
        {
            // ブフラグをリセット
            isDissolve = false;

            // ディゾルブをかける
            StartCoroutine(Dissolve());

            // ディゾルブが終わるまで待機
            while (!isDissolve)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        // 武器を無効化する
        currentWeapon.SetActive(false);


        if (weapontype != WeaponType.None)
        {
            // 新しい武器をセット
            currentWeapon = weaponDict[weapontype];
            currentWeaponType = weapontype;

            // 新しい武器の位置を変更する
            WeaponChangingInit();

            // 新しい武器を有効化する
            currentWeapon.SetActive(true);

            // フラグをリセット
            isDissolve = false;

            // ディゾルブを逆再生
            StartCoroutine(ReturnDissolve());

            // ディゾルブが終わるまで待機
            while (!isDissolve)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        if(rend != null)
        {
            // マテリアルリセット
            rend.material.SetFloat("_DisAmount", 0f);
        }
    }

    // ディゾルブ処理
    public IEnumerator Dissolve()
    {
        for (float disAmount = 0f; disAmount <= 1;)
        {
            // 子オブジェクト（モデルオブジェクト）を取得
            rend = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Renderer>();

            disAmount += Time.deltaTime * DissolveSpeed;

            // マテリアルにセット
            rend.material.SetFloat("_DisAmount", disAmount);

            yield return null;
        }
        isDissolve = true;
    }

    public IEnumerator ReturnDissolve()
    {
        for (float disAmount = 1f; disAmount >=0;)
        {
            // 子オブジェクト（モデルオブジェクト）を取得
            rend = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Renderer>();

            disAmount -= Time.deltaTime * DissolveSpeed;

            // マテリアルにセット
            rend.material.SetFloat("_DisAmount", disAmount);

            yield return null;
        }
        isDissolve = true;
    }


    private void WeaponChangingInit()
    {
        //指定のオブジェクトの子として設定
        currentWeapon.transform.parent = parentObject.transform;

        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
