using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionAnimator
{
    public static bool IsCurrentAnimatorState(this Animator self, string _stateName, int _layerIndex = 0)
    {
        return self.GetCurrentAnimatorStateInfo(_layerIndex).IsName(_stateName);
    }

    /// <summary>
    /// Animatorに指定のParameterが存在しているかチェック
    /// </summary>
    /// <param name="_parameterName">パラメータ名</param>
    /// <param name="type">パラメータのタイプ（型）</param>
    /// <returns></returns>
    public static bool CheckExistsParameter(this Animator self, string _parameterName, AnimatorControllerParameterType type = AnimatorControllerParameterType.Bool)
    {
        bool isExists = false;
        foreach (var parameter in self.parameters)
        {
            if (parameter.type != type) continue;
            if (parameter.name != _parameterName) continue;
            isExists = true;
            break;
        }
        if (isExists == false) Debug.LogError(_parameterName + " Not Found!!");
        return isExists;
    }
}
