using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Collider collider;

    private int startCount;
    private int downCount;
    private bool smash;
    private bool down;
    // Start is called before the first frame update
    void Start()
    {
        startCount = 20;
        downCount = 20;
        smash = true;
        down = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (smash)
        {
            startCount--;
            transform.Translate(0, 0.1f, 0);
        }
        if (startCount < 0)
        {
            smash = false;
            Invoke("DownRock", 1.0f);
        }
        if (down)
        {
            transform.Translate(0, -0.1f, 0);
        }
    }

    void DownRock()
    {
        down = true;
        Invoke("DestroyRock", 2.0f);
    }

    void DestroyRock()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collider.enabled = false;
        }
    }
}
