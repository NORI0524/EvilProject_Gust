using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSummonSystem : MonoBehaviour
{
    //攻撃するたびに
    [SerializeField] private int addSp;

    private SpComponent playerSp = null;

    // Start is called before the first frame update
    void Start()
    {
        playerSp = GameObject.Find("unitychan_dynamic").GetComponent<SpComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            playerSp.AddPoint(addSp);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            playerSp.TriggerUltimate();
        }

        if(playerSp.IsUltimate())
        {

        }
    }
}
