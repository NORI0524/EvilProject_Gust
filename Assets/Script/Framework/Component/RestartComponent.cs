using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class RestartComponent : MonoBehaviour
{
    private Transform initTransform = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Restart()
    {
        transform.Set(initTransform);
    }
}
