using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private int count;
    private bool smash;
    // Start is called before the first frame update
    void Start()
    {
        count = 10;
        smash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (smash)
        {
            count--;
            transform.Translate(0, 0.1f, 0);
        }
        if (count < 0)
        {
            smash = false;
            Invoke("DestroyRock", 1.5f);
        }
    }

    void DestroyRock()
    {
        Destroy(this.gameObject);
    }
}
