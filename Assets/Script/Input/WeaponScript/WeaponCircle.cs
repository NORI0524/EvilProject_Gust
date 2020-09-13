using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCircle : MonoBehaviour
{
    // 回転の速さ
    [SerializeField] float Angle = 2f;
    // 追従するオブジェクト
    [SerializeField] GameObject FollowObj = null;

    void Start()
    {
        if (FollowObj != null)
        {
            transform.position = FollowObj.transform.position;
            transform.localEulerAngles = Vector3.zero;
        }
    }

    void Update()
    {
       
        transform.position = FollowObj.transform.position;
        
        transform.Rotate(0, -Angle, 0);
    }
}
