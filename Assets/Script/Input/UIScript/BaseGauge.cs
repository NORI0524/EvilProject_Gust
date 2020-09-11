using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GaugeType
{
    HpGauge,
    SpGauge,
}

public class BaseGauge : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    [SerializeField] private GaugeType gaugeType = GaugeType.HpGauge;

    private Slider slider = null;
    private BaseStatusComponent statusPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        if (target == null) return;

        switch(gaugeType)
        {
            case GaugeType.HpGauge:
                break;

            case GaugeType.SpGauge:
                statusPoint = target.GetComponent<SpComponent>();
                break;

            default:
                break;
        }

        if(statusPoint != null)
        {
            slider.value = statusPoint.Value;
            slider.minValue = statusPoint.MinValue;
            slider.maxValue = statusPoint.MaxValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)statusPoint.Value;
    }
}
