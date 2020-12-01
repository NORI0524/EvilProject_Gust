using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ComboComponent : MonoBehaviour
{
    [ShowInInspector] private int currentCombo = 0;

    [SerializeField] private float comboJudgeTime = 1.0f;
    private float limitTime = 0.0f;

    [SerializeField, Range(1, 10)] private int everyComboNum = 5;


    // Update is called once per frame
    void Update()
    {
        limitTime += Time.deltaTime;

        if (limitTime > comboJudgeTime)
        {
            currentCombo = 0;
            limitTime = 0.0f;
        }
    }

    public void CountCombo()
    {
        if (limitTime <= comboJudgeTime)
        {
            limitTime = 0.0f;
            currentCombo++;
            if(CheckEveryCombo())
            {
                Debug.Log(currentCombo + "コンボだドン！！");
            }
        }
    }

    private bool CheckEveryCombo()
    {
        return currentCombo != 0 && currentCombo % everyComboNum == 0;
    }
}
