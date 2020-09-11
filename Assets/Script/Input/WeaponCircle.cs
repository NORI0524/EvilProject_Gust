using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCircle : MonoBehaviour
{
    float Angle = 2f;

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, -Angle, 0);
    }
}
