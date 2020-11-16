using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionKeyConfig
{
    public static bool GetKey(this GameKeyConfig _key)
    {
        return Input.GetKey((KeyCode)_key);
    }
    public static bool GetKeyDown(this GameKeyConfig _key)
    {
        return Input.GetKeyDown((KeyCode)_key);
    }
    public static bool GetKeyUp(this GameKeyConfig _key)
    {
        return Input.GetKeyUp((KeyCode)_key);
    }

    public static bool GetKey(this DebugKeyConfig _key)
    {
        return Input.GetKey((KeyCode)_key);
    }
    public static bool GetKeyDown(this DebugKeyConfig _key)
    {
        return Input.GetKeyDown((KeyCode)_key);
    }
    public static bool GetKeyUp(this DebugKeyConfig _key)
    {
        return Input.GetKeyUp((KeyCode)_key);
    }

    /// <summary>
    /// 長押し判定
    /// </summary>
    /// <param name="_second">長押し判定する秒数</param>
    /// <returns></returns>
    public static bool GetKeyLongDown(this DebugKeyConfig _key, float _second)
    {
        bool isLongPress = false;
        float time = 0.0f;

        if (_key.GetKey())
        {
            isLongPress = true;
            time += Time.deltaTime;
        }
        else
        {
            if(time >= _second)
                isLongPress = false;
        }

        return isLongPress;
    }

    //private static IEnumerator LongCount()
    //{

    //    yield return new WaitForSeconds(1);

    //    // 長押しされているかでカウント処理を変える
    //    if (this.downBool == true)
    //    {



    //        if (time == 5)
    //        {

    //            yield break;
    //        }
    //        else
    //        {
    //            // まだ5秒経っていないなら処理を繰り返す
    //            StartCoroutine(LongCount());
    //            yield break;
    //        }

    //    }
    //}
}
