using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public static readonly uint None = 0;
    public static readonly uint Wait = 1 << 0;
    public static readonly uint Move = 1 << 1;
    public static readonly uint Avoid = 1 << 2;
    public static readonly uint Attack = 1 << 3;
    public static readonly uint Attack2 = 1 << 4;
    public static readonly uint Attack3 = 1 << 5;
    public static readonly uint Attack_heavy = 1 << 6;
    public static readonly uint Attack_heavy2 = 1 << 7;
    public static readonly uint Attack_heavy3 = 1 << 8;
}

[RequireComponent(typeof(HpComponent))]
public class PlayerController : BaseComponent
{

    Animator animator = null;

    WeaponManager weaponManager = null;
    WeaponSummonSystem weaponSummonSys = null;

    HpComponent hp = null;

    GameObject summonWeapon = null;

    public BitFlag state = new BitFlag();

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの状態
        state.FoldALLBit();
        state.AddBit(PlayerState.Wait);

        if (TryGetComponent(out animator) == false)
        {
            Debug.LogError("animatorがありません。");
        }

        //weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        weaponSummonSys = GetComponent<WeaponSummonSystem>();

        //summonWeapon = GameObject.Find("SummonWeapon");

        hp = GetComponent<HpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //summonWeapon.SetActive(weaponSummonSys.IsSummon());

        //ダメージ付与
        if (hp.IsDamageTrigger())
        {
            animator.SetTrigger("Damage");
        }

        //回避
        if (state.CheckBit(PlayerState.Avoid) == false)
        {
            if (GameKeyConfig.Avoid.GetKeyDown())
            {
                animator.SetTrigger("Roll");
            }
        }

        //通常攻撃（左クリック）
        if (GameKeyConfig.Attack_Light.GetKeyDown())
        {
            //StartCoroutine(weaponManager.ChangeWeapon(WeaponType.DARK_SWORD));
            animator.SetTrigger("Attack");
        }

        //派生攻撃（右クリック）
        if (GameKeyConfig.Attack_Strong.GetKeyDown())
        {
            //StartCoroutine(weaponManager.ChangeWeapon(WeaponType.HAMMER));
            animator.SetTrigger("Attack2nd");
        }
    }
}
