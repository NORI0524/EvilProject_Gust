using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BarrageComponent : MonoBehaviour
{
    [SerializeField] Transform originTransform = null;
    [SerializeField] GameObject barrageObject = null;

    [TabGroup("Direction")]
    [SerializeField, Range(1, 360)] int directionNum = 4;
    [TabGroup("Object")]
    [SerializeField, Range(1, 30)] int onceDirectionObjectNum = 4;

    [TabGroup("Object")]
    [SerializeField] float betweenSpace = 1.0f;

    [TabGroup("Direction")]
    [SerializeField,PropertyRange(0.0f,360.0f)] float offsetAngle = 0.0f;

    [TabGroup("Direction")]
    [SerializeField] private bool isWayBarrage = false;
    [TabGroup("Direction")]
    [SerializeField, Range(1.0f, 180.0f)]
    [ShowIf("isWayBarrage")]private float wayAngle = 180.0f;

    [TabGroup("Object")]
    [SerializeField] private float offsetHeight = 0.0f;

    [SerializeField] private bool isDelay = false;
    [SerializeField, ShowIf("isDelay")] private float delaySeconds = 1.0f;

    private ObjectPool pool = null;


    private void Awake()
    {
        //とりあえず 方向数×一方向数の2倍オブジェクトをストック
        pool = new ObjectPool(barrageObject, directionNum * onceDirectionObjectNum * 2);
    }

    [HideInEditorMode]
    [Button("Play", ButtonSizes.Large)]
    private void Create()
    {
        StartCoroutine("CreateBarrage");
    }
    public IEnumerator CreateBarrage()
    {
        var fowardVec = originTransform.forward;
        var rangeAngle = isWayBarrage ? wayAngle : 360.0f;
        var onceAngle = rangeAngle / directionNum;
        var fixRangeAngle = isWayBarrage ? (rangeAngle - onceAngle) / 2.0f : 0.0f;

        //for (int directionCount = 0; directionCount < directionNum; directionCount++)
        //{
        //    var angleDegree = (directionCount * onceAngle) - fixRangeAngle + offsetAngle;
        //    var rotatedVec = Quaternion.Euler(0.0f, angleDegree, 0.0f) * fowardVec;

        //    for(int onceObjectCount = 0; onceObjectCount < onceDirectionObjectNum; onceObjectCount++)
        //    {
        //        var objectPos = rotatedVec * betweenSpace * (onceObjectCount + 1);
        //        var barrage = pool.GenerateInstance();
        //        objectPos.y += offsetHeight;
        //        barrage.transform.position = objectPos + originTransform.position;
        //        barrage.transform.rotation = Quaternion.LookRotation(rotatedVec, Vector3.up);
        //    }

        //    yield return new WaitForSeconds(delaySeconds);
        //}

        for (int onceObjectCount = 0; onceObjectCount < onceDirectionObjectNum; onceObjectCount++)
        {
            for (int directionCount = 0; directionCount < directionNum; directionCount++)
            {
                var angleDegree = (directionCount * onceAngle) - fixRangeAngle + offsetAngle;
                var rotatedVec = Quaternion.Euler(0.0f, angleDegree, 0.0f) * fowardVec;
                var objectPos = rotatedVec * betweenSpace * (onceObjectCount + 1);
                var barrage = pool.GenerateInstance();
                objectPos.y += offsetHeight;
                barrage.transform.position = objectPos + originTransform.position;
                barrage.transform.rotation = Quaternion.LookRotation(rotatedVec, Vector3.up);
            }
            if(isDelay)
            {
                yield return new WaitForSeconds(delaySeconds);
            }
        }
    }
}
