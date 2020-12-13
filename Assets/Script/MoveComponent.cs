using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private bool isMoveUp = false;
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private bool isMoveTargetPos = false;
    [SerializeField,ShowIf("isMoveTargetPos")] private float distance = 1.0f;

    private Vector3 target = Vector3.zero;
    private Vector3 vector = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (isMoveTargetPos == false) return;
        var pos = transform.position;
        target = new Vector3(pos.x, pos.y, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        vector = isMoveUp ? transform.up : transform.forward;
        var move = speed * Time.deltaTime;

        if (isMoveTargetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, target + vector * distance, speed);
        }
        else
        {
            transform.Translate(move * vector);
        }
    }
}
