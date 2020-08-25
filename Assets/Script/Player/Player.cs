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
}

public class Player : BaseCompornent
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

    Random random;

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
    }

    // Update is called once per frame
    void Update()
    {
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("Roll", true);
            }
        }

        //if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
        //{
        //    UnityEngine.Debug.Log("Wait!!");
        //    state.FoldBit(PlayerState.Attack);
        //}

        //攻撃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(state.CheckBit(PlayerState.Attack) == false)
            {
                playerAnimator.SetBool("Attack", true);
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

        if (playerAnimator.IsCurrentAnimatorState("Attack_Right_to_Left"))
        {
            playerAnimator.SetBool("Attack", false);
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
