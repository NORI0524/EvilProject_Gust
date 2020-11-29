using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject targetObject = null;
    List<GameObject> objectList;

    public ObjectPool(GameObject _target, int _stockNum = 10)
    {
        if (_target != null)
            targetObject = _target;

        objectList = new List<GameObject>();
        objectList.Clear();

        for (int index = 0; index < _stockNum; index++)
        {
            var obj = GameObject.Instantiate(targetObject) as GameObject;
            obj.SetActive(false);
            objectList.Add(obj);
        }
    }

    public GameObject GenerateInstance()
    {
        var newObject = CheckInactiveObject();
        if(newObject == null)
        {
            newObject = GameObject.Instantiate(targetObject) as GameObject;
            newObject.SetActive(true);
            objectList.Add(newObject);
        }
        else
        {
            newObject.SetActive(true);
        }
        return newObject;
    }

    GameObject CheckInactiveObject()
    {
        GameObject gameObject = null;
        foreach(var obj in objectList)
        {
            if (obj.activeSelf) continue;
            gameObject = obj;
            break;
        }
        return gameObject;
    }
}
