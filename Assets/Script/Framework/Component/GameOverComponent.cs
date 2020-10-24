using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverComponent : MonoBehaviour
{
    [SerializeField,Tooltip("生死判定するオブジェクト")] private GameObject targetObject = null;
    [SerializeField] private GameObject gameoverObject = null;

    HpComponent hp = null;

    private bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if(targetObject.TryGetComponent(out hp) == false)
        {
            Debug.LogError("HpComponentがありません");
            return;
        }
        gameoverObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                var restart = targetObject.GetComponent<RestartComponent>();
                restart.Restart();
                isGameOver = false;
                gameoverObject.SetActive(false);
            }
        }

        if(hp.IsDead())
        {
            isGameOver = true;
            gameoverObject.SetActive(true);
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
