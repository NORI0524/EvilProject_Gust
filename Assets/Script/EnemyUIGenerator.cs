using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 実行時に敵のHPゲージを生成
/// </summary>
public class EnemyUIGenerator : MonoBehaviour
{
    [SerializeField] private float drawRange = 1.0f;

    private GameObject gaugeUI = null;

    private HpComponent hp = null;

    // Start is called before the first frame update
    void Start()
    {
        var targetCanvas = FindObjectOfType<Canvas>();
        var enemyGaugeUI = Resources.Load("Prefabs/UI/EnemyGauge");

        if (enemyGaugeUI == null) return;

        gaugeUI = Instantiate(enemyGaugeUI) as GameObject;

        //オブジェクト名をそのまま反映
        var texts = gaugeUI.GetComponentsInChildren<Text>();
        foreach(var text in texts)
        {
            text.text = gameObject.name;
        }

        //ゲージの設定
        var gauge = gaugeUI.GetComponent<BaseGauge>();
        gauge.targetObject = gameObject;

        var UI_Overlay = gauge.GetComponent<UIController_Overlay>();
        UI_Overlay.targetCanvas = targetCanvas;
        UI_Overlay.targetTransform = gameObject.transform;

        //指定のCanvasに追加
        targetCanvas.gameObject.AddChild(gaugeUI);


        hp = gameObject.GetComponent<HpComponent>();
    }

    private void Update()
    {
        if (hp.IsDead()) return;

        var disVec = transform.position - Camera.main.transform.position;
        bool isDraw = disVec.sqrMagnitude <= drawRange.Pow2();
        gaugeUI.SetActive(isDraw);
        if (isDraw == false) return;
    }
}
