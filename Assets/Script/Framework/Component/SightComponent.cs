using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SightComponent : MonoBehaviour
{
    [SerializeField, Range(0.0f, 360.0f)]
    private float m_searchAngle = 0.0f;

    private SphereCollider m_sphereCollider = null;

    //イベント
    public event System.Action<GameObject> onFound = (obj) => { };
    public event System.Action<GameObject> onLost = (obj) => { };

    public float SearchAngle { get { return m_searchAngle; } }
    public float SearchRadius
    {
        get
        {
            if (m_sphereCollider == null)
            {
                m_sphereCollider = GetComponent<SphereCollider>();
            }
            return m_sphereCollider != null ? m_sphereCollider.radius : 0.0f;
        }
    }


    private float m_searchCosTheta = 0.0f;

    private List<FoundData> m_foundList = new List<FoundData>();

    private void Start()
    {
        m_sphereCollider = GetComponent<SphereCollider>();
        ApplySearchAngle();
    }

    private void Update()
    {
        UpdateFondObject();
    }

    //シリアライズされた値がインスペクター上で変更されたら
    private void OnValidate()
    {
        ApplySearchAngle();
    }

    private void OnDisable()
    {
        m_foundList.Clear();
    }

    private void ApplySearchAngle()
    {
        float searchRad = m_searchAngle * 0.5f * Mathf.Deg2Rad;
        m_searchCosTheta = Mathf.Cos(searchRad);
    }

    //対象がコライダー内
    private void OnTriggerEnter(Collider other)
    {
        GameObject enterObject = other.gameObject;

        //念のため多重登録されないように
        if(m_foundList.Find(value => value.Obj == enterObject) == null)
        {
            m_foundList.Add(new FoundData(enterObject));
        }
    }

    //対象がコライダー外
    private void OnTriggerExit(Collider other)
    {
        GameObject exitObject = other.gameObject;
        var foundData = m_foundList.Find(value => value.Obj == exitObject);
        if (foundData == null)
        {
            return;
        }

        if(foundData.IsCurrentFound())
        {
            onLost(foundData.Obj);
        }

        m_foundList.Remove(foundData);
    }


    private void UpdateFondObject()
    {
        foreach(var foundData in m_foundList)
        {
            GameObject targetObj = foundData.Obj;
            if (targetObj == null) continue;

            bool isFound = CheckFoundObject(targetObj);
            foundData.Update(isFound);

            if(foundData.IsFound())
            {
                onFound(targetObj);
            }
            else if(foundData.IsLost())
            {
                onLost(targetObj);
            }
        }
        Debug.Log(m_foundList.Count);
    }

    private bool CheckFoundObject(GameObject _target)
    {
        Vector3 targetPos = _target.transform.position;
        Vector3 thisPos = transform.position;

        Vector3 thisPosXZ = Vector3.Scale(thisPos, new Vector3(1.0f, 0.0f, 1.0f));
        Vector3 targetPosXZ = Vector3.Scale(targetPos, new Vector3(1.0f, 0.0f, 1.0f));

        Vector3 toTargetFlatDir = (targetPosXZ - thisPosXZ).normalized;
        Vector3 thisForward = transform.forward;


        if (!IsWithingRangeAngle(thisForward, toTargetFlatDir, m_searchCosTheta))
        {
            return false;
        }

        Vector3 toTargetDir = (targetPos - thisPos).normalized;

        if(!IsHitRay(thisPos,toTargetDir,_target))
        {
            return false;
        }

        return true;
    }

    private bool IsWithingRangeAngle(Vector3 _forwardDir, Vector3 _toTargetDir, float _cosTheta)
    {
        //方向ベクトルが無い場合、同位置にあるものと判断
        if (_toTargetDir.sqrMagnitude <= Mathf.Epsilon) return true;

        float dot = Vector3.Dot(_forwardDir, _toTargetDir);
        return dot >= _cosTheta;
    }

    private bool IsHitRay(Vector3 _fromPosition, Vector3 _toTargetDir,GameObject _target)
    {
        //方向ベクトルが無い場合、同位置にあるものと判断
        if (_toTargetDir.sqrMagnitude <= Mathf.Epsilon) return true;

        RaycastHit onHitRay;
        if(!Physics.Raycast(_fromPosition,_toTargetDir, out onHitRay,SearchRadius))
        {
            return false;
        }

        if(onHitRay.transform.gameObject != _target)
        {
            return false;
        }
        return true;
    }
}

public class FoundData
{
    private GameObject m_obj = null;
    private bool isCurrentFound = false;
    private bool isPrevFound = false;

    public FoundData(GameObject _gameObject)
    {
        m_obj = _gameObject;
    }

    public GameObject Obj { get { return m_obj; } }

    public Vector3 Position { get { return Obj != null ? Obj.transform.position : Vector3.zero; } }

    public void Update(bool isFound)
    {
        isPrevFound = isCurrentFound;
        isCurrentFound = isFound;
    }

    public bool IsFound()
    {
        return isCurrentFound && !isPrevFound;
    }
    public bool IsLost()
    {
        return !isCurrentFound && isPrevFound;
    }
    public bool IsCurrentFound()
    {
        return isCurrentFound;
    }
}
