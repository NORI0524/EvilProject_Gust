using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer
{
    class State
    {
        protected BGMPlayer bgmPlayer;
        public State(BGMPlayer _bgmPlayer)
        {
            this.bgmPlayer = _bgmPlayer;
        }

        public virtual void PlayBGM() { }
        public virtual void PauseBGM() { }
        public virtual void StopBGM() { }
        public virtual void UpdateBGM() { }
        public virtual void HadFadeOutBGM() { }
    }

    //各Stateクラス////////////////////////////////////////////////
    class Wait : State
    {
        public Wait(BGMPlayer bgmPlayer) : base(bgmPlayer) { }

        public override void PlayBGM()
        {
            if (bgmPlayer.FadeInTime > 0.0f)
                bgmPlayer.state = new FadeIn(bgmPlayer);
            else
                bgmPlayer.state = new Playing(bgmPlayer);
        }
    }

    class FadeIn : State
    {
        float t = 0.0f;

        public FadeIn(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            bgmPlayer.source.Play();
            bgmPlayer.source.volume = 0.0f;
        }

        public override void PauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void StopBGM()
        {
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }

        public override void UpdateBGM()
        {
            t += Time.deltaTime;
            bgmPlayer.source.volume = bgmPlayer.initVolume * (t / bgmPlayer.FadeInTime);

            if (t >= bgmPlayer.FadeInTime)
            {
                bgmPlayer.source.volume = bgmPlayer.initVolume;
                bgmPlayer.state = new Playing(bgmPlayer);
            }
        }
    }

    class Playing : State
    {
        public Playing(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            if (bgmPlayer.source.isPlaying == false)
            {
                bgmPlayer.source.volume = bgmPlayer.initVolume;
                bgmPlayer.source.Play();
            }
        }

        public override void PauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }

        public override void StopBGM()
        {
            bgmPlayer.state = new FadeOut(bgmPlayer);
        }
    }

    class Pause : State
    {

        State preState;

        public Pause(BGMPlayer bgmPlayer, State preState) : base(bgmPlayer)
        {
            this.preState = preState;
            bgmPlayer.source.Pause();
        }

        public override void StopBGM()
        {
            bgmPlayer.source.Stop();
            bgmPlayer.state = new Wait(bgmPlayer);
        }

        public override void PlayBGM()
        {
            bgmPlayer.state = preState;
            bgmPlayer.source.Play();
        }
    }

    class FadeOut : State
    {
        float initVolume;
        float t = 0.0f;

        public FadeOut(BGMPlayer bgmPlayer) : base(bgmPlayer)
        {
            initVolume = bgmPlayer.source.volume;
        }

        public override void PauseBGM()
        {
            bgmPlayer.state = new Pause(bgmPlayer, this);
        }
        bool FadeFlg;
        public override void UpdateBGM()
        {
            t += Time.deltaTime;
            bgmPlayer.source.volume = initVolume * (1.0f - t / bgmPlayer.FadeOutTime);
            if (t >= bgmPlayer.FadeOutTime)
            {
                FadeFlg = true;
                bgmPlayer.source.volume = 0.0f;
                bgmPlayer.source.Stop();
                bgmPlayer.state = new Wait(bgmPlayer);
            }
            FadeFlg = false;
        }
        public override void HadFadeOutBGM()
        {
            if (FadeFlg)
            {
                bgmPlayer.FadeOutFlg = true;
            }
            else
            {
                bgmPlayer.FadeOutFlg = false;
            }
        }
    }
    /// ////////////////////////////////////////////////////

    GameObject obj;
    AudioSource source;
    State state;
    float FadeInTime = 0.0f;
    float FadeOutTime = 0.0f;
    float initVolume = 1.0f;
    public bool FadeOutFlg;

    public BGMPlayer() { }

    public BGMPlayer(string _bgmFileName, float _volume)
    {
        AudioClip clip = (AudioClip)Resources.Load(_bgmFileName);

        if(clip != null)
        {
            obj = new GameObject("BGMPlayer");
            source = obj.AddComponent<AudioSource>();
            source.clip = clip;
            initVolume = _volume;
            state = new Wait(this);
        }
        else
        {
            Debug.Log("BGM : " + _bgmFileName + " is not found.");
        }
    }

    public void DestroyBGM()
    {
        if (source == null) return;
        GameObject.Destroy(obj);
    }
    public void PlayBGM()
    {
        if (source == null) return;
        state.PlayBGM();
    }
    public void PlayBGM(float _FadeTime)
    {
        if (source == null) return;
        this.FadeInTime = _FadeTime;
        state.PlayBGM();
    }
    public void PauseBGM()
    {
        if (source == null) return;
        state.PauseBGM();
    }
    public void StopBGM()
    {
        if (source == null) return;
        state.StopBGM();
    }
    public void StopBGM(float _FadeTime)
    {
        if (source == null) return;
        this.FadeOutTime = _FadeTime;
        state.StopBGM();
    }
    public void UpdateBGM()
    {
        if (source == null) return;
        state.UpdateBGM();
    }
    public void HadFadeOutBGM(bool FadeOutFlg)
    {
        if (source == null) return;
        state.HadFadeOutBGM();
    }
}
