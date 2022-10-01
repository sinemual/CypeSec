using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton Init
    private static AudioManager _instance;

    void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static AudioManager Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }

    static void Init() // Init script
    {
        _instance = FindObjectOfType<AudioManager>();
        _instance.Initialize();
    }
    #endregion

    public AudioMixer masterMixer;

    public Sound[] sounds;
    private bool checkMusic;
    private bool checkSFX;

    [HideInInspector]
    public AudioSource musicTrack;

    void Initialize()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixer;
        }

        checkMusic = PlayerPrefs.GetInt("s_music") > 0 ? true : false;
        checkSFX = PlayerPrefs.GetInt("s_sound") > 0 ? true : false;
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void AllStop()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null)
                s.source.Stop();
        }
    }

    public void ToggleMusic()
    {
        checkMusic = PlayerPrefs.GetInt("s_music") > 0 ? true : false;
        PlayerPrefs.SetInt("s_music", checkMusic ? 0 : 1);
        if (checkMusic)
            masterMixer.SetFloat("MusicVol", -5f);
        else
            masterMixer.SetFloat("MusicVol", -80f);
    }

    public void ToggleSFX()
    {
        checkSFX = PlayerPrefs.GetInt("s_sound") > 0 ? true : false;
        PlayerPrefs.SetInt("s_sound", checkSFX ? 0 : 1);
        if (checkSFX)
            masterMixer.SetFloat("SFXsVol", -12f);
        else
            masterMixer.SetFloat("SFXsVol", -80f);
    }
}
