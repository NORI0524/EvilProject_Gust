using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

[ExecuteInEditMode, DisallowMultipleComponent]
public class PlayerCameraController : MonoBehaviour
{
    public GameObject target; // an object to follow
    public Vector3 offset; // offset form the target object

    [SerializeField] private float distance = 4.0f; // distance from following object
    [SerializeField] private float polarAngle = 45.0f; // angle with y-axis
    [SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

    [SerializeField] private float minDistance = 1.0f;
    [SerializeField] private float maxDistance = 7.0f;
    [SerializeField] private float minPolarAngle = 5.0f;
    [SerializeField] private float maxPolarAngle = 75.0f;
    [SerializeField] private float mouseXSensitivity = 5.0f;
    [SerializeField] private float mouseYSensitivity = 5.0f;
    [SerializeField] private float scrollSensitivity = 5.0f;

    private GameObject player = null;
    private LockOnTargetDetector lockOnTargetDetector = null;
    private GameObject lockOnTarget = null;

    [SerializeField] protected LockOnCursor lockonCursor;

    [SerializeField] private float search_radius = 20f;
    [SerializeField] private bool IsLockOn = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lockOnTargetDetector = player.GetComponentInChildren<LockOnTargetDetector>();
        lockOnTargetDetector.Search_Radius = search_radius;

    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (lockOnTarget == null)
            {
                // ロックオンターゲットを取得
                GameObject target = lockOnTargetDetector.GetTargetClosestPlayer();

                Debug.Log(target);
                if (target != null)
                {
                    lockOnTarget = target;
                    lockonCursor.OnlockonStart(target.transform);
                }
                IsLockOn = true;
            }
            else
            {
                // すでにターゲットが設定されていた場合は解除
                // 視点リセット
                transform.LookAt(player.transform.eulerAngles);

                lockOnTarget = null;
                lockonCursor.OnlockonEnd();
                IsLockOn = false;
                Debug.Log("ロックオン解除");
            }
        }

        if (IsLockOn == false)
        {
            //マウスのミドルボタンで変更（デバッグのため）
            if (Input.GetKey(KeyCode.Mouse2))
            {
                updateAngle(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                updateDistance(Input.GetAxis("Mouse ScrollWheel"));
            }

            var lookAtPos = target.transform.position + offset;
            updatePosition(lookAtPos);
            transform.LookAt(lookAtPos);
        }

        if (lockOnTarget)
        {
            var lookAtPos = target.transform.position + offset;
            updatePosition(lookAtPos);
            lockOnTargetObject(lockOnTarget);
        }
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseXSensitivity;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseYSensitivity;
        polarAngle = Mathf.Clamp(y, minPolarAngle, maxPolarAngle);
    }

    void updateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, minDistance, maxDistance);
    }

    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;
        var dp = polarAngle * Mathf.Deg2Rad;
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }

    private void lockOnTargetObject(GameObject target)
    {
        Debug.Log("オブジェクトの方向く");
        transform.LookAt(target.transform, Vector3.up);
    }

}