using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDelete : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            foreach (Transform item in this.gameObject.transform)
            {
                item.gameObject.SetActive(true);
            }
        }
        if (Input.GetKey(KeyCode.C))
        {
            foreach (Transform item in this.gameObject.transform)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
