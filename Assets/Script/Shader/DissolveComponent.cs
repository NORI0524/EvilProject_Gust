using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveComponent : MonoBehaviour
{

    [SerializeField] float Speed = 1.0f;
    [SerializeField] float ReturnSpeed = 1.0f;

    private Renderer renderer = null;
    private bool isFinish = false;

    private void Start()
    {
        // 子オブジェクト（モデルオブジェクト）を取得
        renderer = transform.GetChild(0).GetComponent<Renderer>();
    }

    public bool IsFinish
    {
        get { return isFinish; }
    }

    public IEnumerator Dissolve()
    {
        isFinish = false;
        for (float disAmount = 0f; disAmount <= 1;)
        {
            disAmount += Time.deltaTime * Speed;

            // マテリアルにセット
            foreach (var material in renderer.materials)
            {
                material.SetFloat("_DisAmount", disAmount);
            }

            yield return null;
        }
        isFinish = true;
    }

    public IEnumerator ReturnDissolve(GameObject _weapon)
    {
        for (float disAmount = 1f; disAmount >= 0;)
        {
            disAmount -= Time.deltaTime * ReturnSpeed;

            // マテリアルにセット
            foreach (var material in renderer.materials)
            {
                material.SetFloat("_DisAmount", disAmount);
            }

            yield return null;
        }
        isFinish = true;
    }
}
