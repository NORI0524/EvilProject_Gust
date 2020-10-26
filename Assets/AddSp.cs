using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSp : MonoBehaviour
{
    private WeaponSummonSystem weaponSp = null;
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        weaponSp = player.GetComponent<WeaponSummonSystem>();
    }

    public void Add()
    {
        weaponSp.AddSp();
    }
}
