using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サウンド管理用クラス
public class SoundManager
{
    //SEパス
    public const string GameSceneSePath = "Sounds/SE/GameScene_SE/";        //ゲームシーン全般
    public const string TapSePath = "Sounds/SE/Tap_SE/";                    //タップ
    public const string SpaSePath = "Sounds/SE/Spa_SE/";                    //油温泉
    public const string CharaSePath = "Sounds/SE/Character_SE/";            //キャラクター
    public const string OilSplashSePath = "Sounds/SE/OilSplash_SE/";        //油跳ね
    public const string FlamSePath = "Sounds/SE/Flam_SE/";                  //炎
    public const string PotLidSePath = "Sounds/SE/PotLid_SE/";              //鍋蓋
    public const string ThermometerSePath = "Sounds/SE/Thermometer_SE/";    //温度計
    public const string TenkasuSePath = "Sounds/SE/Tenkasu_SE/";            //天かす
    public const string EndOfDaySePath = "Sounds/SE/EndOfDay_SE/";          //一日の終わり
    public const string StampSePath = "Sounds/SE/Stamp_SE/";                //リザルトのスタンプ
    public const string TitleSePath = "Sounds/SE/Title_SE/";                //タイトル
    public const string ResultSePath = "Sounds/SE/Result_SE/";              //リザルト


    //BGMパス
    public const string GameBgmPath = "Sounds/BGM/Game_BGM/";
    public const string TitleBgmPath = "Sounds/BGM/Title_BGM/";
    public const string ResultBgmPath = "Sounds/BGM/Result_BGM/";

    BGMPlayer CurBGMPlayer = null;
    BGMPlayer FadeOutBGMPlayer = null;

    //オーディオ情報クラス
    private class AudioClipInfo
    {
        public string resourceName;
        public string name;
        public AudioClip clip;

        public SortedList<int, SEInfo> stockList = new SortedList<int, SEInfo>();
        public List<SEInfo> playingList = new List<SEInfo>();
        public int maxSENum = 10;
        public float initVolume = 1.0f;
        public float attenuate = 0.0f;

        //コンストラクタ
        public AudioClipInfo(string _resourceName, string _name, int _maxSENum, float _initVolume)
        {
            this.resourceName = _resourceName;
            this.name = _name;

            this.maxSENum = _maxSENum;
            this.initVolume = _initVolume;
            attenuate = CalcAttenuateRate();

            // create stock list
            for (int i = 0; i < maxSENum; i++)
            {
                SEInfo seInfo = new SEInfo(i, 0.0f, initVolume * Mathf.Pow(attenuate, i));
                stockList.Add(seInfo.index, seInfo);
            }
        }

        float CalcAttenuateRate()
        {
            float n = maxSENum;
            return NewtonMethod.run(
                delegate (float p)
                {
                    return (1.0f - Mathf.Pow(p, n)) / (1.0f - p) - 1.0f / initVolume;
                },
                delegate (float p)
                {
                    float ip = 1.0f - p;
                    float t0 = -n * Mathf.Pow(p, n - 1.0f) / ip;
                    float t1 = (1.0f - Mathf.Pow(p, n)) / ip / ip;
                    return t0 + t1;
                },
                0.9f, 100
            );
        }
    }
    class SEInfo
    {
        public int index;
        public float curTime;
        public float volume;

        public SEInfo(int i, float v1, float v2)
        {
            index = i;
            curTime = v1;
            volume = v2;
        }
    }

    GameObject soundPlayer;
    AudioSource audioSource;
    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();


    //コンストラクタ
    public SoundManager()
    {
        //ここにゲーム全体で使う音源を登録
        // なぜかMaxSENumを2以上にしないとvolume1.0でしか音が鳴らなくなる
        //SE

        //BGM

    }

    //SEを再生
    public bool PlaySE(string seName)
    {
        //指定のSEが登録されていない場合
        if (!audioClips.ContainsKey(seName)) return false;

        AudioClipInfo info = audioClips[seName];

        //ロード
        if (info.clip == null)
        {
            info.clip = (AudioClip)Resources.Load(info.resourceName);
        }

        if(soundPlayer == null)
        {
            soundPlayer = new GameObject("SoundPlayer");
            audioSource = soundPlayer.AddComponent<AudioSource>();
        }

        float len = info.clip.length;
        if (info.stockList.Count > 0)
        {
            SEInfo seInfo = info.stockList.Values[0];
            seInfo.curTime = len;
            info.playingList.Add(seInfo);

            // remove from stock
            info.stockList.Remove(seInfo.index);

            // Play SE
            audioSource.PlayOneShot(info.clip, seInfo.volume);
            Debug.Log("SE_Volume : " + seInfo.volume);

            return true;
        }
        return false;
    }

    public void Update()
    {
        // playing SE update
        foreach (AudioClipInfo info in audioClips.Values)
        {
            List<SEInfo> newList = new List<SEInfo>();

            foreach (SEInfo seInfo in info.playingList)
            {
                seInfo.curTime = seInfo.curTime - Time.deltaTime;
                if (seInfo.curTime > 0.0f)
                    newList.Add(seInfo);
                else
                    info.stockList.Add(seInfo.index, seInfo);
            }
            info.playingList = newList;
        }

        //BGM
        CurBGMPlayer.UpdateBGM();


        if (FadeOutBGMPlayer == null) return;
        FadeOutBGMPlayer.UpdateBGM();
    }

    //BGMを再生
    public void PlayBGM(string bgmName, float fadeTime)
    {
        //前のBGMを破棄
        if (FadeOutBGMPlayer != null) FadeOutBGMPlayer.DestroyBGM();

        //現在BGMを再生していたらフェードアウト
        if (CurBGMPlayer != null)
        {
            CurBGMPlayer.StopBGM(fadeTime);
            FadeOutBGMPlayer = CurBGMPlayer;
        }

        // play new BGM
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // null BGM
            CurBGMPlayer = new BGMPlayer();
        }
        else
        {
            CurBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName, audioClips[bgmName].initVolume);
            CurBGMPlayer.PlayBGM(fadeTime);
        }
    }

    public void PlayBGM()
    {
        if (CurBGMPlayer != null && CurBGMPlayer.FadeOutFlg == false)
            CurBGMPlayer.PlayBGM();
        if (FadeOutBGMPlayer != null && FadeOutBGMPlayer.FadeOutFlg == false)
            FadeOutBGMPlayer.PlayBGM();
    }

    public void PauseBGM()
    {
        if (CurBGMPlayer != null) CurBGMPlayer.PauseBGM();
        if (FadeOutBGMPlayer != null) FadeOutBGMPlayer.PauseBGM();
    }

    public void StopBGM(float fadeTime)
    {
        if (CurBGMPlayer != null) CurBGMPlayer.StopBGM(fadeTime);
        if (FadeOutBGMPlayer != null) FadeOutBGMPlayer.StopBGM(fadeTime);
    }
}
