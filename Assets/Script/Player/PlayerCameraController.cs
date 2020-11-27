using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode, DisallowMultipleComponent]
public class PlayerCameraController : MonoBehaviour
{
    [TabGroup("Settings"), SerializeField] private GameObject target; // an object to follow
    [TabGroup("Settings"), SerializeField] private Vector3 offset; // offset form the target object

    [TabGroup("Settings"), PropertyRange("DistanceMin", "DistanceMax"), SerializeField] private float distance = 4.0f; // distance from following object
    [TabGroup("Settings"), PropertyRange("PolarAngleMin", "PolarAngleMax"), SerializeField] private float polarAngle = 45.0f; // angle with y-axis
    [TabGroup("Settings"), SerializeField] private float azimuthalAngle = 45.0f; // angle with x-axis

    [TabGroup("Settings"), MinMaxSlider(0.0f, 20.0f, true), SerializeField] private Vector2 distanceRange = new Vector2(1.0f, 7.0f);
    [TabGroup("Settings"), MinMaxSlider(0.0f, 180.0f, true), SerializeField] private Vector2 polarAngleRange = new Vector2(5.0f, 75.0f);
    [TabGroup("Settings"), SerializeField] private Vector2 mouseSensitivity = new Vector2(5.0f, 5.0f);
    [TabGroup("Settings"), SerializeField] private float scrollSensitivity = 5.0f;

    private GameObject player = null;
    private LockOnTargetDetector lockOnTargetDetector = null;
    private GameObject lockOnTarget = null;

    [TabGroup("LockOn"), SerializeField] protected LockOnCursor lockonCursor;

    [TabGroup("LockOn"), SerializeField] private float search_radius = 20f;
    [TabGroup("LockOn"), SerializeField] private bool IsLockOn = false;

    private float DistanceMin { get { return distanceRange.x; } }
    private float DistanceMax { get { return distanceRange.y; } }
    private float PolarAngleMin { get { return polarAngleRange.x; } }
    private float PolarAngleMax { get { return polarAngleRange.y; } }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lockOnTargetDetector = player.GetComponentInChildren<LockOnTargetDetector>();
        lockOnTargetDetector.Search_Radius = search_radius;
    }

    void FixedUpdate()
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
                    IsLockOn = true;
                }
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
            //プレイヤーから見た敵のベクトルとカメラから見たプレイヤーのベクトルを取得
            var targetToPlayerVec = lockOnTarget.transform.position - player.transform.position;
            var playerToCameraVec = player.transform.position - gameObject.transform.position;

            //XZ平面にするためyを無視
            targetToPlayerVec.y = 0.0f;
            playerToCameraVec.y = 0.0f;

            //角度の差分を計算して加算
            var fixAzimuthalAngle = Vector3.SignedAngle(targetToPlayerVec, playerToCameraVec, Vector3.up);
            azimuthalAngle += fixAzimuthalAngle;

            var lookAtPos = target.transform.position + offset;
            updatePosition(lookAtPos);
            lockOnTargetObject(lockOnTarget);
        }
    }

    void updateAngle(float x, float y)
    {
        x = azimuthalAngle - x * mouseSensitivity.x;
        azimuthalAngle = Mathf.Repeat(x, 360);

        y = polarAngle + y * mouseSensitivity.y;
        polarAngle = Mathf.Clamp(y, polarAngleRange.x, polarAngleRange.y);
    }

    void updateDistance(float scroll)
    {
        scroll = distance - scroll * scrollSensitivity;
        distance = Mathf.Clamp(scroll, distanceRange.x, distanceRange.y);
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
        transform.LookAt(target.transform, Vector3.up);
    }
}