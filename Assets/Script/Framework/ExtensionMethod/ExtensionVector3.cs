using UnityEngine;
using UnityEditor;
using UnityScript.Steps;
using System.Runtime.CompilerServices;

public static class ExtensionVector3
{
    public static Vector3 Parse(this Vector3 vector, string str)
    {
        var elements = str.Trim('(', ')').Split(',');
        var vec = Vector3.zero;
        if (elements.Length <= 3)
        {
            vec.x = float.Parse(elements[0]);
            vec.y = float.Parse(elements[1]);
            vec.z = float.Parse(elements[2]);
        }
        return vec;
    }
}