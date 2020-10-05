using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeValue
{
    private float curValue;
    private int time;
    private float frame;
    public bool isPlus;

    public float minValue;
    public float maxValue;
    public float nowValue;

    public FadeValue(float current, int oneWayTime, float min, float max)
    {
        curValue = current;
        minValue = min;
        maxValue = max;
        frame = current / (max - min);
        time = oneWayTime;
        isPlus = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        frame += 1.0f / time / 60.0f;
        if (isPlus)
        {
            curValue = Mathf.Lerp(minValue, maxValue, frame);
        }
        else
        {
            curValue = Mathf.Lerp(maxValue, minValue, frame);
        }
        if (frame >= 1.0f)
        {
            isPlus = !isPlus;
            frame = 0.0f;
        }
    }

    public float GetCurrentValue() { return curValue; }
}
