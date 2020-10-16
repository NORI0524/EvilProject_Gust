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
}
