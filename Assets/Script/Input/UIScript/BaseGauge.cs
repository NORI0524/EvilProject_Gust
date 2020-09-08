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

    private Slider slider = null;

    private SpComponent sp = null;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        if (target != null)
        {
            sp = target.GetComponent<SpComponent>();
            slider.value = 0;
            slider.minValue = 0;
            slider.maxValue = sp.MaxSp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)sp.Sp;
    }
}
