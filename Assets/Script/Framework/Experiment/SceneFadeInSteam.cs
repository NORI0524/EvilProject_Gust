using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeInSteam : BaseCompornent
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
        nowCount = 0;
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        frame++;
        if(frame > FadeFrame)
        {
            frame = 0;
            if (nowCount < SteamNum)
            {
                //湯気生成
                GameObject obj = Instantiate(prefab) as GameObject;
                obj.transform.position = new Vector3(0, 0, 0);
                nowCount++;
            }
        }

        if(nowCount == SteamNum)
        {
            isFinish = true;
            Destroy(gameObject);
        }
    }
}
