using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponent : BaseComponent
{
    //オブジェクトの種類
    [SerializeField] private GameObject[] obj_Prefab = null;

    //１画面に表示する最大数
    [SerializeField] private int ObjDispMax = 5;

    //現在のオブジェクト数
    [SerializeField] private int currentNum;

    //１度にスポーンさせる数
    [SerializeField] private int onceSpawnNum = 1;

    //スポーン頻度（確率：パーセント）
    [SerializeField] private float spawnFrequency = 100.0f;

    //スポーンするまでのスパン
    [SerializeField] private float spanSeconds = 2.0f;

    //スポーンタイマー
    Timer spanTimer;

    //スポーンする範囲
    [SerializeField] public float spawnDistance = 1.0f;


    ////スポーン用座標2D
    //public float randX_Min;
    //public float randX_Max;
    //public float randY_Min;
    //public float randY_Max;
    //public float dispZ;
    ////Zソート用加算
    //private const float zSortValue = 0.0005f;


    private bool isActive;

    Random random = new Random();


    // Start is called before the first frame update
    void Start()
    {
        spanTimer = new Timer(spanSeconds);
        spanTimer.Start();
        spanTimer.EnabledLoop();
        currentNum = 0;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        spanTimer.Update();
        if (!spanTimer.IsFinish()) return;
        if (currentNum >= ObjDispMax) return;

        //スポーンするかどうか
        if(random.IsInsidePercent(spawnFrequency))
        {
            //スポーン処理
            for (int cnt = 0; cnt < onceSpawnNum; cnt++)
            {
                int select = Random.Range(0, obj_Prefab.Length);
                GameObject obj = Instantiate(obj_Prefab[select]) as GameObject;
                obj.transform.position = transform.position;

                //指定範囲内にスポーン
                var addPos = random.InsideCicleRange(spawnDistance);
                obj.transform.Translate(addPos);

                //float px = Random.Range(randX_Min, randX_Max);
                //float py = Random.Range(randY_Min, randY_Max);
                //obj.transform.position = new Vector3(px, py, dispZ - (zSortValue * currentNum));

                currentNum++;
            }
        }
    }

    public int GetCurrentNum() { return currentNum; }
    public void Decrease() { currentNum--; }

    //設定関係
    public void SetSpawnFrequency(float value)
    {
        spawnFrequency = Mathf.Clamp(value, 0.0f, 100.0f);
    }
    public void SetSpawnSeconds(float value)
    {
        spanSeconds = Mathf.Max(value, 0);
    }
    public void SetOnceSpawnNum(int value)
    {
        onceSpawnNum = Mathf.Max(value, 1);
    }

    public void OnActive() { isActive = true; }
    public void OffActive() { isActive = false; }
}
