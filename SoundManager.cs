using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [SerializeField]
    private AudioSource bgmSource;
    [SerializeField]
    private AudioSource soundSource;

    private Dictionary<string, AudioClip> loadedClip = new Dictionary<string, AudioClip>();

    public bool soundPlay;
    public bool bgmPlay;

    public void SetSoundOn()
    {
        soundPlay = true;
        PlayerPrefs.SetString("SoundSet", "true");
        PlayerPrefs.Save();
        UIPlay.instance.SetSoundToggle(true);
    }

    public void SetSoundOff()
    {
        soundPlay = false;
        PlayerPrefs.SetString("SoundSet", "false");
        PlayerPrefs.Save();
        UIPlay.instance.SetSoundToggle(false);
    }

    public void SetBGMOn()
    {
        bgmPlay = true;
        bgmSource.volume = 1;
        PlayerPrefs.SetString("BGMSet", "true");
        PlayerPrefs.Save();
        UIPlay.instance.SetBGMToggle(true);
    }

    public void SetBGMOff()
    {
        bgmPlay = false;
        bgmSource.volume = 0;
        PlayerPrefs.SetString("BGMSet", "false");
        PlayerPrefs.Save();
        UIPlay.instance.SetBGMToggle(false);
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        bool playerPrefscheck = PlayerPrefs.HasKey("BGMSet");
        string audioSet;


        if (playerPrefscheck)
        {
            audioSet = PlayerPrefs.GetString("BGMSet");
            Debug.Log(audioSet);
            if (audioSet == "true")
            {
                SetBGMOn();
                UIPlay.instance.SetBGMToggle(true);
            }
            else if (audioSet == "false")
            {
                SetBGMOff();
                UIPlay.instance.SetBGMToggle(false);
            }
        }
        else
        {
            SetBGMOn();
            UIPlay.instance.SetBGMToggle(true);
        }


        playerPrefscheck = PlayerPrefs.HasKey("SoundSet");

        if (playerPrefscheck)
        {
            audioSet = PlayerPrefs.GetString("SoundSet");
            Debug.Log(audioSet);
            if (audioSet == "" || audioSet == "true")
            {
                SetSoundOn();
                UIPlay.instance.SetSoundToggle(true);
            }
            else if (audioSet == "false")
            {
                SetSoundOff();
                UIPlay.instance.SetSoundToggle(false);
            }
        }
        else
        {
            SetSoundOn();
            UIPlay.instance.SetSoundToggle(true);
        }

        PlayerPrefs.Save();

        PlayBGM("field01");
    }

    private AudioClip LoadAudioClip(string fullPath)
    {
        AudioClip clip = null;

        if (loadedClip.TryGetValue(fullPath, out clip))
            return clip;    //이미 로드한 것

        clip = Resources.Load<AudioClip>(fullPath); //로드
        if (clip == null)
            return null;    //없음

        loadedClip.Add(fullPath, clip); //로드 추가
        return clip;
    }


    public void PlayBGM(string path)
    {
        AudioClip clip = LoadAudioClip(GetBGMFullPath(path));
        if (clip == null) return;
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySound(string path)
    {
        if (!soundPlay)
            return;
        AudioClip clip = LoadAudioClip(GetSoundFullPath(path));
        if (clip == null) return;
        soundSource.PlayOneShot(clip);
    }

    public void LoadBGM(string path)
    {
        LoadAudioClip(GetBGMFullPath(path));
    }

    public void LoadSound(string path)
    {
        LoadAudioClip(GetSoundFullPath(path));
    }

    private static string GetBGMFullPath(string path)
    {
        return Define.bgmRoot + "/" + path;
    }

    private static string GetSoundFullPath(string path)
    {
        return Define.soundRoot + "/" + path;
    }

    public void ClearLoadedAudioClip()
    {
        loadedClip.Clear();
    }
}
