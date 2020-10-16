using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        if(TryGetComponent(out animator) == false)
        {
            Debug.LogError("animatorがありません。");
        }

        //weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        //weaponSummonSys = GetComponent<WeaponSummonSystem>();

        //summonWeapon = GameObject.Find("SummonWeapon");

        hp = GetComponent<HpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //summonWeapon.SetActive(weaponSummonSys.IsSummon());

        //ダメージ付与
        if (GameKeyConfig.Item.GetKeyDown())
        {
            hp.AddDamage(100);
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
        }

        //派生攻撃（右クリック）
        if (GameKeyConfig.Attack_Strong.GetKeyDown())
        {
            animator.SetTrigger("Attack2nd");
        }
    }
}
