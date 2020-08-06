using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCompornent
{
    [SerializeField]
    float movePower = 1.0f;

    [SerializeField]
    float cameraMove = 0.1f;

    Camera playerCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        var obj = GameObject.Find("Main Camera");
        playerCamera = obj.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var rot = transform.rotation;
            rot.x = playerCamera.transform.rotation.x;

            var move = transform.forward * movePower;
            transform.Translate(move);
        }
        if (Input.GetKey(KeyCode.S))
        {
            var move = transform.forward * movePower * -1;
            transform.Translate(move);
        }
        if (Input.GetKey(KeyCode.A))
        {
            var move = transform.right * movePower * - 1;
            transform.Translate(move);
        }
        if (Input.GetKey(KeyCode.D))
        {
            var move = transform.right * movePower;
            transform.Translate(move);
        }

        //カメラ操作
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var axis = transform.up * - 1;
            playerCamera.transform.RotateAround(Position, axis, cameraMove);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            var axis = transform.up;
            playerCamera.transform.RotateAround(Position, axis, cameraMove);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            var axis = transform.right * -1;
            playerCamera.transform.RotateAround(Position, axis, cameraMove);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            var axis = transform.right;
            playerCamera.transform.RotateAround(Position, axis, cameraMove);
        }
    }
}
