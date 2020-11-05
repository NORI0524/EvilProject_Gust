using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit : MonoBehaviour
{
    [SerializeField] private GameObject targetObject = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        var pos = this.transform.position;
        var hitPos = other.ClosestPointOnBounds(pos);
        var hitVec = pos - hitPos;
        var quaternion = Quaternion.LookRotation(hitVec);
        var createObject = GameObject.Instantiate(targetObject);
        createObject.transform.position = hitPos;
        createObject.transform.rotation = quaternion;
    }
}
