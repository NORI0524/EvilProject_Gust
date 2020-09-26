using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RestartComponent : MonoBehaviour
{
    private GameObject restartObject = null;
    private HpComponent restartHp = null;

    // Start is called before the first frame update
    void Start()
    {
        restartObject = new GameObject("Restart_" + gameObject.name);
        restartObject.transform.Set(transform);

        if(TryGetComponent(out restartHp) == false)
        {
            Debug.LogError("HpComponentがありません。");
        }
    }

    public void Restart()
    {
        transform.Set(restartObject.transform);
        if(restartHp != null)
        {
            restartHp.Restart();
        }
    }
}
