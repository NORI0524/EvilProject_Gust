using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    private Renderer rend;

    private float disAmount = 0.0f;
    bool isDissolve = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public IEnumerator Dissolve(float cnt)
    {
        for(float test=0f;test<=1;)
        {
            // ディゾルブ処理
            // 子オブジェクトを取得
            rend = transform.GetChild(0).gameObject.GetComponent<Renderer>();

            disAmount += Time.deltaTime;

            // マテリアルにセット
            rend.material.SetFloat("_DisAmount", disAmount);

            yield return null;
        }
        
    }
}
