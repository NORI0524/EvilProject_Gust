using UnityEngine;

public class ColliderHit : MonoBehaviour
{
    [SerializeField] private GameObject targetObject = null;
    [SerializeField, Range(1, 100)] private int stockNum = 5;

    ObjectPool pool = null;

    private void Awake()
    {
        pool = new ObjectPool(targetObject, stockNum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        var pos = this.transform.position;
        var hitPos = other.ClosestPointOnBounds(pos);
        var hitVec = pos - hitPos;
        var createObject = pool.GenerateInstance();
        createObject.transform.position = hitPos;

        if(hitVec != Vector3.zero)
        {
            var quaternion = Quaternion.LookRotation(hitVec);
            createObject.transform.rotation = quaternion;
        }
    }
}
