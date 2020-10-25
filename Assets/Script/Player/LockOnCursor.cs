using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ロックオンカーソルを制御するクラス
/// </summary>
public class LockOnCursor : MonoBehaviour
{
    protected RectTransform rectTransform;
    protected Image image;
    protected Transform LockonTarget { get; set; }

    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();

        Debug.Log(rectTransform);
        image = this.GetComponent<Image>();
        image.enabled = false;
        Debug.Log(image);
    }

    void Update()
    {
        if (image.enabled)
        {
            rectTransform.Rotate(0, 0, 1f);

            if (LockonTarget != null)
            {
                Debug.Log(LockonTarget.position);
                Debug.Log(LockonTarget.name);
                Vector3 targetPoint = Camera.main.WorldToScreenPoint(LockonTarget.position);
                rectTransform.position = targetPoint;
            }
        }
    }

    public void OnlockonStart(Transform target)
    {
        image.enabled = true;
        LockonTarget = target;
    }

    public void OnlockonEnd()
    {
        image.enabled = false;
        LockonTarget = null;
    }
}
