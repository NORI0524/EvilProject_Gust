using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledParticlePlay : MonoBehaviour
{
    private void OnEnable()
    {
        var particle = gameObject.GetComponent<ParticleSystem>();
        //子のパーティクルも同時に再生
        particle.Play(true);
    }
}
