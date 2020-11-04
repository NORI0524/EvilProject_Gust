using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSounds : MonoBehaviour
{
    private ParticleSystem particle = null;
    private AudioSource particleAudio = null;

    // Start is called before the first frame update
    private void Start()
    {
        if(!TryGetComponent(out particle))
        {
            Debug.LogError(particle.GetType().Name + "がありません");
        }

        if (!TryGetComponent(out particleAudio))
        {
            Debug.LogError(particleAudio.GetType().Name + "がありません");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (particle.isPlaying)
        {
            if(!particleAudio.isPlaying)
                particleAudio.Play();
        }
        else
        {
            particleAudio.Stop();
        }
    }
}
