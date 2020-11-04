using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveComponent : MonoBehaviour
{

    [SerializeField, Range(0.0f, 10.0f)] float Speed = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] float ReturnSpeed = 1.0f;

    private void Awake()
    {
        // 子オブジェクト（モデルオブジェクト）を取得
        Renderer = transform.GetChild(0).GetComponent<Renderer>();
    }

    public bool IsFinish { get; private set; } = false;

    public Renderer Renderer { get; private set; } = null;

    public IEnumerator Dissolve()
    {
        IsFinish = false;
        for (float disAmount = 0f; disAmount <= 1;)
        {
            disAmount += Time.deltaTime * Speed;

            // マテリアルにセット
            foreach (var material in Renderer.materials)
            {
                material.SetFloat("_DisAmount", disAmount);
            }

            yield return null;
        }
        IsFinish = true;
    }

    public IEnumerator ReturnDissolve()
    {
        for (float disAmount = 1f; disAmount >= 0;)
        {
            disAmount -= Time.deltaTime * ReturnSpeed;

            // マテリアルにセット
            foreach (var material in Renderer.materials)
            {
                material.SetFloat("_DisAmount", disAmount);
            }

            yield return null;
        }
        IsFinish = true;
    }
}
