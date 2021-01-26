using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LockOnTargetDetector : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private float search_radius;
    private GameObject player = null;
    public float Search_Radius
    {
        set { search_radius = value; }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public GameObject GetTargetClosestPlayer()
    {
        var hits = Physics.SphereCastAll(player.transform.position, search_radius, player.transform.forward, 0.01f, LayerMask.GetMask("Default")).Select(h => h.transform.gameObject).ToList();

        hits = FilterTargetObject(hits);

        if (0 < hits.Count())
        {
            float min_target_distance = float.MaxValue;
            GameObject target = null;

            foreach (var hit in hits)
            {
                Vector3 targetScreenPoint = Camera.main.WorldToViewportPoint(hit.transform.position);
                float target_distance = Vector2.Distance(new Vector2(0.5f, 0.5f),new Vector2(targetScreenPoint.x, targetScreenPoint.y));

                Debug.Log(hit.gameObject + ": " + target_distance);

                if (target_distance < min_target_distance)
                {
                    min_target_distance = target_distance;
                    target = hit.transform.gameObject;
                }            }
            return target;

        }
        else
        {
            return null;
        }
    }

    protected List<GameObject> FilterTargetObject(List<GameObject> hits)
    {
        return hits.Where(h =>
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
            Debug.Log(screenPoint);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        }).Where(h => h.tag == "Enemy").ToList();
    }

}
