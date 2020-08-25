using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionAnimator
{
    public static bool IsCurrentAnimatorState(this Animator self, string _stateName, int _layerIndex = 0)
    {
        return self.GetCurrentAnimatorStateInfo(_layerIndex).IsName(_stateName);
    }
}
