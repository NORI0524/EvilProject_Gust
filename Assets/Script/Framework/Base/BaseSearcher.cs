using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSearcher : MonoBehaviour
{
    private List<GameObject> m_targets = new List<GameObject>();

    protected bool isFound;

    protected GameObject target = null;

    // Start is called before the first frame update

    protected void Init()
    {
        var searching = GetComponentInChildren<SightComponent>();
        searching.onFound += OnFound;
        searching.onLost += OnLost;
    }

    private void OnFound(GameObject _foundObject)
    {
        m_targets.Add(_foundObject);
        if (_foundObject.name == "unitychan")
        {
            isFound = true;
            target = _foundObject;
        }
    }

    private void OnLost(GameObject _lostObject)
    {
        m_targets.Remove(_lostObject);

        if (m_targets.Count == 0)
        {
            isFound = false;
        }
    }
}
