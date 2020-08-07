using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    int InitObjectMax = 10;

    [SerializeField]
    GameObject targetObject = null;

    List<GameObject> objectList;

    // Start is called before the first frame update
    void Start()
    {
        objectList = new List<GameObject>();
        objectList.Clear();

        for(int index = 0; index < InitObjectMax; index++)
        {
            var obj = Instantiate(targetObject) as GameObject;
            obj.SetActive(false);
            objectList.Add(obj);
        }
    }

    public GameObject GenerateInstance()
    {
        var newObject = CheckInactiveObject();
        if(newObject == null)
        {
            newObject = Instantiate(targetObject) as GameObject;
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
