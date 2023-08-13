using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using OcularInk.Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    private AudioMixerData mixerData;

    public float FxVolume
    {
        get => PlayerPrefs.GetFloat("FxVolume", 1f);
        private set => PlayerPrefs.SetFloat("FxVolume", value);
    }

    public float MusicVolume
    {
        get => PlayerPrefs.GetFloat("MusicVolume", 1f);
        private set => PlayerPrefs.SetFloat("MusicVolume", value);
    }

    private AudioClip latestClip;

    private Dictionary<string, AudioClip> loadedClips = new Dictionary<string, AudioClip>();

    private float latestPlayTime;

    public bool MuteSfx
    {
        get => PlayerPrefs.GetInt("muteSfx", 0) == 1;
        set => PlayerPrefs.SetInt("muteSfx", value ? 1 : 0);
    }

    public bool MuteMusic
    {
        get => PlayerPrefs.GetInt("muteMusic", 0) == 1;
        set => PlayerPrefs.SetInt("muteMusic", value ? 1 : 0);
    }

    public bool Vibration
    {
        get => PlayerPrefs.GetInt("vibration", 1) == 1;
        set => PlayerPrefs.SetInt("vibration", value ? 1 : 0);
    }

    protected override void Awake()
    {
        base.Awake();
        if (Instance == this)
        {
            Initialize();
        }
    }

    public void PlayAudio(string id, float pitch = 1)
    {
        if (latestClip?.name == id && Time.time - latestPlayTime < 0.1f)
            return;

        if (MuteSfx)
            return;

        latestPlayTime = Time.time;

        sfxSource.pitch = pitch;
        AudioClip clip = LoadAudio($"SFX/{id}");
        sfxSource.PlayOneShot(clip);
    }

    public void PlayAtPosition(string id, Vector3 pos)
    {
        if (latestClip?.name == id && Time.time - latestPlayTime < 0.1f)
            return;

        AudioClip clip = LoadAudio($"SFX/{id}");
        AudioSource.PlayClipAtPoint(clip, pos, FxVolume);
    }

    /// <summary>
    /// Sets the mute of main audio source
    /// </summary>
    /// <param name="value">0 = Unmute | 1 = Mute</param>
    public bool ToggleMuteSfx()
    {
        MuteSfx = !MuteSfx;
        sfxSource.mute = MuteSfx;
        return MuteSfx;
    }

    public void SetMusic(string id)
    {
        AudioClip clip = LoadAudio(id);

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    public bool ToggleMuteMusic()
    {
        MuteMusic = !MuteMusic;
        musicSource.mute = MuteMusic;
        return MuteMusic;
    }

    public bool ToggleVibration()
    {
        Vibration = !Vibration;
        return Vibration;
    }

    public void PlayButton()
    {
        PlayAudio("Button");
        Vibrate(VibrationType.Selection);
    }

    public void PlayBackButton()
    {
        PlayAudio("BackButton");
        Vibrate(VibrationType.Selection);
    }

    public void Initialize()
    {
        // If no child, then create audio sources
        if (transform.childCount == 0)
        {
            mixerData = Resources.Load<AudioMixerData>("AudioMixerData");

            sfxSource = new GameObject("SfxSource").AddComponent<AudioSource>();
            musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();

            sfxSource.outputAudioMixerGroup = mixerData.sfxGroup;
            musicSource.outputAudioMixerGroup = mixerData.musicGroup;

            sfxSource.transform.parent = transform;
            musicSource.transform.parent = transform;

            mixerData.baseMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, MusicVolume)) * 20);
            mixerData.baseMixer.SetFloat("SfxVolume", Mathf.Log10(Mathf.Max(0.0001f, FxVolume)) * 20);
        }

        sfxSource.mute = MuteSfx;
        musicSource.mute = MuteMusic;

        SceneManager.sceneLoaded += OnSceneChange;
    }

    public void Stop()
    {
        sfxSource.Stop();
    }

    public void PreloadAudio(string id)
    {
        LoadAudio(id);
    }

    private AudioClip LoadAudio(string id)
    {
        // IF Audio is not same with previously cached audio, then load it
        if (latestClip == null || latestClip?.name != id)
        {
            // Before loading, check if it exists in loaded clips dictionary
            if (loadedClips.ContainsKey(id))
            {
                latestClip = loadedClips[id];
            }
            else
            {
                latestClip = Resources.Load<AudioClip>(id);
                loadedClips.Add(id, latestClip);
            }
        }

        return latestClip;
    }

    public void ToggleMusic(bool value)
    {
        musicSource.time = 0;
        if (value)
            musicSource.Play();
        else
            musicSource.Stop();
    }

    public float GetAudioDuration(string clipName)
    {
        return LoadAudio(clipName).length;
    }

    public void Vibrate(VibrationType vibration)
    {
        if (!Vibration)
        {
            return;
        }

        switch (vibration)
        {
            case VibrationType.Selection:
                Taptic.Selection();
                break;
            case VibrationType.Light:
                Taptic.Light();
                break;
            case VibrationType.Medium:
                Taptic.Medium();
                break;
            case VibrationType.Heavy:
                Taptic.Heavy();
                break;
        }
    }

    public float GetRemainingMusicTime()
    {
        Debug.Log($"Clip: {musicSource.clip?.name}");
        return musicSource.clip.length - musicSource.time;
    }

    public void CrossFadeMusic(string newMusicId)
    {
        var newClip = LoadAudio($"BGM/{newMusicId}");
        musicSource.DOFade(0f, 0.5f).onComplete = () =>
        {
            musicSource.clip = newClip;
            musicSource.Play();
            musicSource.DOFade(1f, 0.5f);
        };
    }

    public void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        // if (musicSource.isPlaying && scene.buildIndex == (int) GameScene.Game)
        // {
        //     musicSource.Stop();
        // }
        // else if (!musicSource.isPlaying && scene.buildIndex == (int) GameScene.Home)
        // {
        //     musicSource.Play();
        // }
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        mixerData.baseMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 20);
    }

    public void SetFxVolume(float value)
    {
        FxVolume = value;
        mixerData.baseMixer.SetFloat("SfxVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 20);
    }
}

public enum VibrationType
{
    Selection,
    Light,
    Medium,
    Heavy
}