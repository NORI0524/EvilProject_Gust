using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクトを取得
        rend = transform.GetChild(0).gameObject.GetComponent<Renderer>();
    }

    public bool Dissolve()
    {
        rend.material.SetFloat("_DisAmount", disAmount);
    }
    
}
