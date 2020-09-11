using RPGCharacterAnims;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    GameObject refObj;
    WeaponManager wpnmng;

    // Start is called before the first frame update
    void Start()
    {
        refObj = GameObject.Find("WeaponManager");
        wpnmng = refObj.GetComponent<WeaponManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(wpnmng);
            StartCoroutine(wpnmng.ChangeWeapon(WeaponType.DARK_SWORD));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log(wpnmng);
            StartCoroutine(wpnmng.ChangeWeapon(WeaponType.AXE));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log(wpnmng);
            StartCoroutine(wpnmng.ChangeWeapon(WeaponType.HAMMER));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log(wpnmng);
            StartCoroutine(wpnmng.ChangeWeapon(WeaponType.MACE));
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log(wpnmng);
            StartCoroutine(wpnmng.ChangeWeapon(WeaponType.SKULLAXE));
        }
    }
}
