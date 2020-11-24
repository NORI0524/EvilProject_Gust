using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchObjectComponent : MonoBehaviour
{
    [SerializeField] private TagData targetTag = TagData.None;

    public List<GameObject> triggerObjects { get; } = new List<GameObject>();


    public GameObject ClosestObject
    {
        get
        {
            var parentObject = gameObject.transform.parent.gameObject;
            return parentObject.FindClosestGameObjects(triggerObjects.ToArray());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enterObject = other.gameObject;
        if (enterObject.CompareTag(targetTag.ToString()) == false) return;
        if (triggerObjects.Contains(enterObject)) return;
        triggerObjects.Add(enterObject);
    }

    private void OnTriggerExit(Collider other)
    {
        var exitObject = other.gameObject;
        if (exitObject.CompareTag(targetTag.ToString()) == false) return;
        if (triggerObjects.Contains(exitObject) == false) return;
        triggerObjects.Remove(exitObject);
    }
}
