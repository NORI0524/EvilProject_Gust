using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollBack : MonoBehaviour
{
    ParticleSystem particle = null;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {

        //　Particle型のインスタンス生成
        var enter = new List<ParticleSystem.Particle>();

        //　Enterのパーティクルを取得
        int numEnter = particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //　データがあればキャラクターに接触した
        if (numEnter != 0)
        {
            Debug.Log("接触");
        }
    }
}
