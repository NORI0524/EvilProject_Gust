using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(SphereCollider))]
public class GravityComponent : MonoBehaviour
{
    [InfoBox("Rigidbodyがアタッチされたオブジェクトだけ力を受ける")]
    [SerializeField, Range(0.0f, 100.0f)] private float forcePower = 1.0f;

    [SerializeField] bool isRepulsion = false;

    [SerializeField]
    [InfoBox("ForceModeについて\n " +
        "Force：継続的に力を加える（質量を考慮）\n" +
        "Acceleration：継続的に力を加える（質量を無視）\n" +
        "Impulse：瞬間的に力を加える（質量を考慮）\n" +
        "VelocityChange：瞬間的に力を加える（質量を無視）")]
    ForceMode forceMode = ForceMode.Force;

    [SerializeField] TagData targetTag = TagData.None;

    private void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        var obj = other.gameObject;

        if(targetTag != TagData.None)
        {
            if (obj.CompareTag(targetTag.ToString()) == false) return;
        }

        var targetRigid = obj.GetComponent<Rigidbody>();
        if (targetRigid == null) return;

        var forceVec = gameObject.transform.position - obj.transform.position;
        forceVec *= isRepulsion ? -1.0f : 1.0f;
        targetRigid.AddForce(forceVec.normalized * forcePower, forceMode);
    }
}
