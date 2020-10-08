using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionRigitbody
{
    /// <summary>
    /// 指定した位置・回転の軸が固定されているか判定
    /// </summary>
    /// <param name="constraints">指定する位置・回転</param>
    /// <returns></returns>
    public static bool CheckConstraints(this Rigidbody rigidbody, RigidbodyConstraints constraints)
    {
        return rigidbody.constraints.HasFlag(constraints);
    }

    /// <summary>
    ///  指定した位置・回転の軸を固定or固定解除
    /// </summary>
    /// <param name="constraints">指定する位置・回転</param>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetConstraints(this Rigidbody rigidbody, RigidbodyConstraints constraints, bool enable)
    {
        bool isEnabled = rigidbody.CheckConstraints(constraints);

        if (enable)
        {
            if (isEnabled) return;
            rigidbody.constraints |= constraints;
        }
        else
        {
            if (isEnabled == false) return;
            rigidbody.constraints -= constraints;
        }
    }

    /// <summary>
    ///  位置のX軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezePositionX(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezePositionX, enable);
    }
    /// <summary>
    ///  位置のY軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezePositionY(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezePositionY, enable);
    }
    /// <summary>
    ///  位置のZ軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezePositionZ(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezePositionZ, enable);
    }

    /// <summary>
    ///  回転のX軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezeRotationX(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezeRotationX, enable);
    }
    /// <summary>
    ///  回転のY軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezeRotationY(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezeRotationY, enable);
    }
    /// <summary>
    ///  回転のZ軸を固定or固定解除
    /// </summary>
    /// <param name="enable">固定する：true　解除：false</param>
    public static void SetFreezeRotationZ(this Rigidbody rigidbody, bool enable)
    {
        rigidbody.SetConstraints(RigidbodyConstraints.FreezeRotationZ, enable);
    }
}
