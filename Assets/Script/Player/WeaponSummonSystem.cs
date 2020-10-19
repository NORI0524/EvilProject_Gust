﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSummonSystem : MonoBehaviour
{
    //攻撃するたびに
    [SerializeField] private int addSp;

    [SerializeField] private GameObject effectObject = null;

    private SpComponent playerSp = null;

    // Start is called before the first frame update
    void Start()
    {
        playerSp = GameObject.Find(gameObject.name).GetComponent<SpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //武器召喚発動
        if(Input.GetKeyDown(KeyCode.Q))
        {
            playerSp.TriggerUltimate();
        }

        if(effectObject != null)
        {
            effectObject.SetActive(playerSp.IsUltimate());
        }
    }

    public void AddSp()
    {
        playerSp.AddPoint(addSp);
    }

    public bool IsSummon()
    {
        return playerSp.IsUltimate();
    }
}
