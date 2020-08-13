using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動の入力
        Vector3 vec = Vector3.zero;

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

        //動いているなら
        if(vec != Vector3.zero)
        {
            playerAnimator.SetBool("Run", true);
        }
        else
        {
            playerAnimator.SetBool("Run", false);
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
