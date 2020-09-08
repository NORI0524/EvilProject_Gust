using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    GameObject refObj;

    // Start is called before the first frame update
    void Start()
    {
        refObj = GameObject.Find("WeaponManager");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WeaponManager wpnmng = refObj.GetComponent<WeaponManager>();

            Debug.Log(wpnmng);
            wpnmng.ChangeWeapon(WeaponType.AXE);
        }
    }
}
