using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class SceneFadeOutSteam : BaseCompornent
{
    [SerializeField] GameObject prefab = null;

    [SerializeField] int SteamNum = 15;

    int nowCount;
    bool isStart;
    bool isFinish;

    int frame = 0;
    [SerializeField] int FadeFrame = 15;

    public bool IsStart { set { isStart = value; } }
    public bool IsFinish { get { return isFinish; } }

    // Start is called before the first frame update
    void Start()
    {
        for(nowCount = 1; nowCount <= SteamNum; nowCount++)
        {
            //湯気生成
            GameObject obj = Instantiate(prefab) as GameObject;
            obj.transform.position = new Vector3(0, 0, 0.01f * -nowCount);
            obj.name = "SceneSteam_" + nowCount;
        }
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        frame++;
        if (frame > FadeFrame)
        {
            frame = 0;

            for (nowCount = 1; nowCount <= SteamNum; nowCount++)
            {
                var obj = GameObject.Find("SceneSteam_" + nowCount);
                var color = obj.GetComponent<Renderer>().material.color;

                color.a = Mathf.Max(color.a - 0.1f, 0.0f);

                if (color.a == 0.0f) isFinish = true;
                obj.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
