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
    Animator weaponAnimator = null;
    Animator weaponAnimator2 = null;

    CharaMoveComponent charaMove = null;

    WeaponSummonSystem weaponSummonSys = null;

    HpComponent hp = null;

    GameObject summonWeapon = null;
    GameObject summonWeapon2 = null;

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

        charaMove = GetComponent<CharaMoveComponent>();

        weaponSummonSys = GetComponent<WeaponSummonSystem>();

        summonWeapon = GameObject.Find("SummonWeapon");
        summonWeapon2 = GameObject.Find("SummonWeapon2");
        weaponAnimator = summonWeapon.GetComponentInChildren<Animator>();
        weaponAnimator2 = summonWeapon2.GetComponentInChildren<Animator>();

        hp = GetComponent<HpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        var isSummon = weaponSummonSys.IsSummon();
        summonWeapon.SetActive(isSummon);
        summonWeapon2.SetActive(isSummon);

        //死亡
        if (hp.IsDead())
        {
            animator.SetBool("Death", true);
            charaMove.enabled = false;
        }

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
            animator.SetTrigger("Attack");

            if (isSummon)
            {
                weaponAnimator2.SetTrigger("Attack");
            }
        }

        //派生攻撃（右クリック）
        if (GameKeyConfig.Attack_Strong.GetKeyDown())
        {
            animator.SetTrigger("Attack2nd");
            if (isSummon)
            {
                weaponAnimator.SetTrigger("Attack");
            }
        }

        //派生攻撃２（同時クリック）
        if(GameKeyConfig.Attack_Light.GetKeyDown() && GameKeyConfig.Attack_Strong.GetKeyDown())
        {
            animator.SetTrigger("Attack3rd");
        }
    }
}
