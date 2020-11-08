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
    [SerializeField] private Image fillImage = null;
    [SerializeField] private GaugeType gaugeType = GaugeType.HpGauge;
    [SerializeField] private bool isDeadVisible = false;

    private BaseStatusComponent statusPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) return;

        switch(gaugeType)
        {
            case GaugeType.HpGauge:
                statusPoint = target.GetComponent<HpComponent>();
                break;

            case GaugeType.SpGauge:
                statusPoint = target.GetComponent<SpComponent>();
                break;

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (statusPoint != null && fillImage != null)
        {
            fillImage.fillAmount = (float)statusPoint.Value / statusPoint.MaxValue;
        }

        if(isDeadVisible && fillImage.fillAmount == 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    public GameObject targetObject
    {
        set
        {
            target = value;
        }
    }
}
