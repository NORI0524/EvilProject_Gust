using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCamera : MonoBehaviour
{
    private GameObject player = null;
    private LockOnTargetDetector lockOnTargetDetector = null;
    private GameObject lockOnTarget = null;

    [SerializeField] protected LockOnCursor lockonCursor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lockOnTargetDetector = player.GetComponentInChildren<LockOnTargetDetector>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (lockOnTarget == null)
            {
                // ロックオンターゲットを取得
                GameObject target = lockOnTargetDetector.GetTargetClosestPlayer();

                if (target != null)
                {
                    lockOnTarget = target;
                    lockonCursor.OnlockonStart(target.transform);
                }
            }
            else
            {
                // すでにターゲットが設定されていた場合は解除
                // 視点リセット
                transform.LookAt(player.transform.eulerAngles);

                lockOnTarget = null;
                lockonCursor.OnlockonEnd();
            }
        }

        if (lockOnTarget)
        {
            lockOnTargetObject(lockOnTarget);
        }
    }

    private void lockOnTargetObject(GameObject target)
    {
        transform.LookAt(target.transform, Vector3.up);
    }

}
