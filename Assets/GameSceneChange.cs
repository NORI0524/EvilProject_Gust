using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneChange : MonoBehaviour
{
    [SerializeField] string filePath = "";

    public void ChangeGameScene()
    {
        var sceneChange = GameObject.Find("SceneChange").GetComponent<sceneChangeManager>();

        if (sceneChange == null)
        {
            Debug.LogError("NULL参照です");
            return;
        }
        StartCoroutine(sceneChange.ChangeScene(filePath));
    }
}
