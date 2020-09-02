using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int UDkey = 0;
        int RLKey = 0;
        if (Input.GetKey(KeyCode.UpArrow)) { UDkey = 1; }
        if (Input.GetKey(KeyCode.DownArrow)) { UDkey = -1; }
        if (Input.GetKey(KeyCode.RightArrow)) { RLKey = 1; }
        if (Input.GetKey(KeyCode.LeftArrow)) { RLKey = -1; }

        if (UDkey != 0)
        {
            transform.position += new Vector3(0, 0, UDkey * moveSpeed);
        }
        if (RLKey != 0)
        {
            transform.position += new Vector3(RLKey * moveSpeed, 0, 0);
        }
    }
}
