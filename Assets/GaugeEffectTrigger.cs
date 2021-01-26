using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeEffectTrigger : MonoBehaviour
{
    private Image image = null;
    [SerializeField] GameObject gaugeEffect = null;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        gaugeEffect.SetActive(image.fillAmount >= 1.0f);
    }
}
