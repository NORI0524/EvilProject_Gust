using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField]
    private SphereCollider searchArea;

    private Enemy e_con;
    [SerializeField]
    private float searchAngle = 50f;

    private void Start()
    {
        e_con = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //　主人公の方向
            var playerDirection = other.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //　サーチする角度内だったら発見
            if (angle <= searchAngle)
            {
                e_con.Attack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            e_con.nAttack();
        }
    }

    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        Handles.color = new Vector4(0, 1, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
}
