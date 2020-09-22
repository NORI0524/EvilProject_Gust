using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
public class Player : BaseComponent
{
    [SerializeField]
    float movePower = 1.0f;

    [SerializeField]
    float rotateSpeed = 1.0f;

    [SerializeField]
    float cameraMove = 0.1f;

    Camera playerCamera = null;
    Rigidbody playerRigidbody = null;
    Animator playerAnimator = null;

    WeaponManager weaponManager = null;
    WeaponSummonSystem weaponSummonSys = null;

    Random random;

    GameObject summonWeapon = null;

    public BitFlag state = new BitFlag();

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの状態
        state.FoldALLBit();
        state.AddBit(PlayerState.Wait);


        playerCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();

        weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
        weaponSummonSys = GetComponent<WeaponSummonSystem>();

        summonWeapon = GameObject.Find("SummonWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        summonWeapon.SetActive(weaponSummonSys.IsSummon());

        //移動の入力
        Vector3 vec = Vector3.zero;


        if(state.CheckBit(PlayerState.Attack) == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                vec.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                vec.z -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                vec.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                vec.x += 1;
            }
        }

        //正規化
        vec.Normalize();

        //動いているなら
        if(vec != Vector3.zero)
        {
            state.AddBit(PlayerState.Move);
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            state.FoldBit(PlayerState.Move);
            playerAnimator.SetBool("Run", false);
        }

        //回避
        if(state.CheckBit(PlayerState.Avoid) == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("Roll", true);
            }
        }

        //if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
        //{
        //    UnityEngine.Debug.Log("Wait!!");
        //    state.FoldBit(PlayerState.Attack);
        //}

        //通常攻撃（左クリック）
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(state.CheckBit(PlayerState.Attack) == false)
            {
                StartCoroutine(weaponManager.ChangeWeapon(WeaponType.DARK_SWORD));
                playerAnimator.SetBool("Attack", true);

                var anime = summonWeapon.GetComponentInChildren<Animator>();
                anime.SetBool("isAttack", true);
            }
            else
            {
                if(state.CheckBit(PlayerState.Attack2) == false)
                {
                    playerAnimator.SetBool("Attack2", true);
                }
                else
                {
                    playerAnimator.SetBool("Attack3", true);
                }
            }
        }

        //派生攻撃（右クリック）
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (state.CheckBit(PlayerState.Attack2))
            {
                if (state.CheckBit(PlayerState.Attack_heavy) == false)
                {
                    StartCoroutine(weaponManager.ChangeWeapon(WeaponType.HAMMER));
                    playerAnimator.SetBool("Attack_heavy", true);
                }
                else
                {
                    if(state.CheckBit(PlayerState.Attack_heavy2) == false)
                    {
                        playerAnimator.SetBool("Attack_heavy2", true);
                    }
                    else
                    {
                        playerAnimator.SetBool("Attack_heavy3", true);
                    }
                }
            }
        }


        if (playerAnimator.IsCurrentAnimatorState("Attack_Right_to_Left"))
        {
            playerAnimator.SetBool("Attack", false);
            var anime = summonWeapon.GetComponentInChildren<Animator>();
            anime.SetBool("isAttack", false);
        }

        if (playerAnimator.IsCurrentAnimatorState("Attack_Left_to_Right"))
        {
            playerAnimator.SetBool("Attack2", false);
        }

        if (playerAnimator.IsCurrentAnimatorState("Attack_Right_to_foward"))
        {
            playerAnimator.SetBool("Attack3", false);
        }
        if(playerAnimator.IsCurrentAnimatorState("Roll_Forward"))
        {
            playerAnimator.SetBool("Roll", false);
        }
        if(playerAnimator.IsCurrentAnimatorState("Attack_heavy"))
        {
            playerAnimator.SetBool("Attack_heavy", false);
        }
        if (playerAnimator.IsCurrentAnimatorState("Attack_heavy2"))
        {
            playerAnimator.SetBool("Attack_heavy2", false);
        }
        if (playerAnimator.IsCurrentAnimatorState("Attack_heavy3"))
        {
            playerAnimator.SetBool("Attack_heavy3", false);
        }

        ////カメラ操作
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    var axis = transform.up * -1;
        //    playerCamera.transform.RotateAround(Position, axis, cameraMove);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    var axis = transform.up;
        //    playerCamera.transform.RotateAround(Position, axis, cameraMove);
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    var axis = transform.right * -1;
        //    playerCamera.transform.RotateAround(Position, axis, cameraMove);
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    var axis = transform.right;
        //    playerCamera.transform.RotateAround(Position, axis, cameraMove);
        //}

        //カメラ方向からx-z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * vec.z + playerCamera.transform.right * vec.x;

        //方向
        if (moveForward != Vector3.zero)
        {
            //float angle = Vector3.SignedAngle(transform.forward, moveForward.normalized, transform.up);
            //var U_Angle = Mathf.Abs(angle);
            //if (U_Angle > 0.5f)
            //{
            //    Debug.Log(U_Angle);
            //    transform.Rotate(transform.up, U_Angle);
            //}

            var moveQua = Quaternion.LookRotation(moveForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveQua, Time.deltaTime * rotateSpeed);

        }
        var move = moveForward * movePower;
        //移動
        playerRigidbody.AddForce(move);
        //transform.Translate(move);
    }
}
