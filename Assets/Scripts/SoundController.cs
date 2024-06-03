using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;

    public AudioSource fxAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource carAudioSource;

    [Header("Temple Escape Sounds")]
    public AudioClip GameMusic;
    public AudioClip LoseSound;
    public AudioClip CrashSound;
    public AudioClip FlipSound;
    public AudioClip CarSound;
    public AudioClip Token;
    public AudioClip Token2;
    public AudioClip button;
    public AudioClip back;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        if (fxAudioSource == null)
        {
            fxAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
            musicAudioSource = transform.GetChild(1).GetComponent<AudioSource>();

        }
        //if (Constants.getAudioConfig() == 1)
        //{
        //    unmute();
        //}
        //else
        //{
        //    mute();
        //}

    }

    void OnEnable()
    {
        //AudioController.OnGameSoundStopEvent += OnGameSoundStop;
    }

    void OnDisable()
    {
        //AudioController.OnGameSoundStopEvent -= OnGameSoundStop;
    }

    void OnGameSoundStop()
    {
        onlyMute();
        Destroy(instance);
        Destroy(gameObject);
    }

    void Start()
    {

    }
    public void playSound(AudioClip clip, bool isLoop, AudioSource audio)
    {
        //if (Constants.getAudioConfig() == 0)
        //    return;

        //if (clip == null)
        //{
        //    Constants.log(tag, "playSound() sound not exist");
        //    return;
        //}

        if (isLoop)
        {
            stopFxSound(audio);
            fxAudioSource.clip = clip;
            fxAudioSource.loop = true;
            fxAudioSource.Play();
        }
        else
        {
            audio.PlayOneShot(clip, 1f);
        }

    }

    public void stopFxSound(AudioSource audio)
    {

        audio.Stop();
    }

    public void stopMusicSound()
    {
        musicAudioSource.Stop();
    }

    public void playMusic(AudioClip clip)
    {

        stopMusicSound();
        musicAudioSource.clip = clip;
        musicAudioSource.loop = true;
        //if (Constants.getAudioConfig() == 0)
        //{
        //    return;

        //}
        musicAudioSource.Play();
    }

    public void mute()
    {
        //Constants.setAudioConfig(0);
        musicAudioSource.mute = true;
        fxAudioSource.mute = true;
    }

    public void onlyMute()
    {
        musicAudioSource.mute = true;
        fxAudioSource.mute = true;
    }

    public void unmute()
    {
        //Constants.setAudioConfig(1);
        musicAudioSource.mute = false;
        fxAudioSource.mute = false;
    }

    public bool isMuted()
    {
        return musicAudioSource.mute;
    }

    
}
