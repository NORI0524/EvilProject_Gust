using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharaMoveComponent : MonoBehaviour
{
    Camera camera = null;
    Rigidbody rigidbody = null;
    Animator animator = null;

    [SerializeField,Tooltip("振り向く速度")]float rotateSpeed = 1.0f;
    [SerializeField, Tooltip("移動量")] float movePower = 1.0f;
    [SerializeField] string animationBoolName = "Run";

    private bool isMove = false;

    public bool IsMove
    {
        get
        {
            return isMove;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動の入力
        Vector3 vec = Vector3.zero;

        if (GameKeyConfig.Forward.GetKey())
        {
            vec.z += 1;
        }
        if (GameKeyConfig.Back.GetKey())
        {
            vec.z -= 1;
        }
        if (GameKeyConfig.Left.GetKey())
        {
            vec.x -= 1;
        }
        if (GameKeyConfig.Right.GetKey())
        {
            vec.x += 1;
        }

        //正規化
        vec.Normalize();

        //動いているなら
        if (vec != Vector3.zero)
        {
            isMove = true;
            if (animator != null)
                animator.SetBool(animationBoolName, true);
        }
        else
        {
            isMove = false;
            if (animator != null)
                animator.SetBool(animationBoolName, false);
        }

        //カメラ方向からx-z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        Vector3 moveForward = cameraForward * vec.z + camera.transform.right * vec.x;

        //方向
        if (moveForward != Vector3.zero)
        {
            var moveQua = Quaternion.LookRotation(moveForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveQua, Time.deltaTime * rotateSpeed);

        }

        if(animator.applyRootMotion == false)
        {
            var move = moveForward * movePower;
            //移動
            rigidbody.AddForce(move);
        }
    }
}
