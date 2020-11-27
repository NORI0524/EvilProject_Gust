using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BarrageComponent : MonoBehaviour
{
    [SerializeField] Transform originTransform = null;
    [SerializeField] GameObject barrageObject = null;

    [SerializeField, Range(1, 360)] int directionNum = 4;
    [SerializeField, Range(1, 30)] int onceDirectionObjectNum = 4;

    [SerializeField] float betweenSpace = 1.0f;

    [SerializeField,PropertyRange(0.0f,360.0f)] float offsetAngle = 0.0f;

    [SerializeField] private bool isWayBarrage = false;
    [SerializeField, Range(1.0f, 180.0f)]
    [ShowIf("isWayBarrage")]private float wayAngle = 180.0f;

    [SerializeField] private float offsetHeight = 0.0f;


    //private void Update()
    //{
    //    if(GameKeyConfig.Jump.GetKeyDown())
    //        CreateBarrage();
    //}

    [HideInEditorMode]
    [Button("Play",ButtonSizes.Large)]
    public void CreateBarrage()
    {
        var fowardVec = originTransform.forward;
        var rangeAngle = isWayBarrage ? wayAngle : 360.0f;
        var onceAngle = rangeAngle / directionNum;
        var fixRangeAngle = isWayBarrage ? (rangeAngle - onceAngle) / 2.0f : 0.0f;

        for (int directionCount = 0; directionCount < directionNum; directionCount++)
        {
            var angleDegree = (directionCount * onceAngle) - fixRangeAngle + offsetAngle;
            var rotatedVec = Quaternion.Euler(0.0f, angleDegree, 0.0f) * fowardVec;

            for(int onceObjectCount = 0; onceObjectCount < onceDirectionObjectNum; onceObjectCount++)
            {
                var objectPos = rotatedVec * betweenSpace * (onceObjectCount + 1);
                var barrage = Instantiate(barrageObject);
                objectPos.y += offsetHeight;
                barrage.transform.position = objectPos + originTransform.position;
            }
        }
    }
}
