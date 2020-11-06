using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Collider))]
public class Monitor : MonoBehaviour
{
    [SerializeField]
    private LayerMask obstacleLayer;    // 障害物を指定
    private GameObject player;
    private Navigation nav;
    private SphereCollider searchArea;
    private Enemy e_con;

    [SerializeField]
    private float searchAngle = 130f;

    public void Start()
    {
        nav = transform.parent.GetComponent<Navigation>();
        e_con = transform.parent.GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        searchArea = this.gameObject.GetComponent<SphereCollider>();
    }

    private void Update()
    {
        // 検知したもの(ここではPlayerへレイを飛ばす)
        Debug.DrawLine(transform.position + Vector3.up, player.transform.position + Vector3.up, Color.blue);

        if (Physics.Linecast(transform.position + Vector3.up, player.transform.position + Vector3.up, obstacleLayer))
        {
            e_con.EndDiscover();
        }

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
            if (angle <= searchAngle && !Physics.Linecast(transform.position + Vector3.up, player.transform.position + Vector3.up, obstacleLayer))
            {
                e_con.Discover();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            e_con.EndDiscover();
        }
    }

    //　サーチする角度表示
    private void OnDrawGizmos()
    {
        Handles.color = new Vector4(1, 0, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
}
