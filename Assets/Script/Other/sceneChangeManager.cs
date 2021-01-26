using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeManager : MonoBehaviour
{
    [SerializeField] string filePath = "";

    public void Change()
    {
        var sceneChange = GameObject.Find("SceneChange").GetComponent<sceneChangeManager>();

        if (sceneChange == null)
        {
            Debug.LogError("NULL参照です");
            return;
        }
        StartCoroutine(sceneChange.ChangeScene(filePath));
    }


    // フェード用のマテリアル
    [SerializeField] Material fademateiral;
    bool isFade = false;

    /// <summary>
    /// シーン偏移
    /// </summary>
    /// <param name="filepas">ビルドセッティングで設定したファイルパス</param>
    /// <returns></returns>
    public IEnumerator ChangeScene(string filepas)
    {
        // sceneが切り替わっても破棄されないようにする
        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(FadeOut());
        // フェードが終わるまで待機
        while(!isFade)
        {
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(filepas);

        StartCoroutine(FadeIn());
        while (isFade)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut()
    {
        for (float degree = 0.0f; degree < 1.0f;)
        {
            degree += Time.deltaTime;
            Debug.Log(degree);

            // マテリアルに値をセット
            fademateiral.SetFloat("_Degree", degree);

            yield return null;
        }
        isFade = true;
    }

    public IEnumerator FadeIn()
    {
        for (float degree = 1.0f; degree > 0.0f;)
        {
            degree -= Time.deltaTime;
            Debug.Log(degree);

            // マテリアルに値をセット
            fademateiral.SetFloat("_Degree", degree);

            yield return null;
        }
        isFade = true;
    }

}
