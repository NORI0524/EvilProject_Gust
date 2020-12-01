using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitComponent : MonoBehaviour
{
    [SerializeField] private GameObject orbitObject = null;
    [SerializeField, Range(1, 10)] private int orbitNum = 1;
    [SerializeField, Range(0.0f, 1000.0f)] private float orbitSpeed = 90.0f;
    [SerializeField, Range(0.0f, 30.0f)] private float radius = 2.0f;
    [SerializeField] private float heightOffset = 0.0f;

    [SerializeField] private bool isReverse = false;

    private List<GameObject> orbitList = new List<GameObject>();
    private float rotateAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        for(int index = 0; index < orbitNum; index++)
        {
            var orbit = Instantiate(orbitObject);
            orbitList.Add(orbit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var baseObj = gameObject;
        var angleRate = 360.0f / orbitNum;
        int count = 0;
        foreach(var orbit in orbitList)
        {
            var angle = rotateAngle + (angleRate * count);
            var pos = baseObj.transform.position + Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward * radius;
            orbit.transform.position = pos + new Vector3(0.0f, heightOffset, 0.0f);

            var toVec = orbit.transform.position - baseObj.transform.position;
            orbit.transform.rotation = Quaternion.LookRotation(toVec, Vector3.up);

            count++;
        }

        var signedAngle = isReverse ? -1.0f : 1.0f;
        rotateAngle += orbitSpeed * Time.deltaTime * signedAngle;
        rotateAngle = Mathf.Repeat(rotateAngle, 360.0f);
    }
}
