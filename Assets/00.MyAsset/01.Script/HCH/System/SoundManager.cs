using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton

    static SoundManager instance;
    public static SoundManager Instance => instance ? instance : new GameObject("SoundManager").AddComponent<SoundManager>();

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Variable

    [SerializeField] AudioClip[] buttonClickSounds;

    AudioSource[] audioSource = new AudioSource[3];
    AudioSource bgmSource;

    public float effectVolume { get; private set; } = 1;

    #endregion

    #region Property

    public AudioClip[] ButtonClickSounds
    {
        get
        {
            if (buttonClickSounds != null)
            {
                return buttonClickSounds;
            }
            else
            {
                AudioClip[] tempClip = new AudioClip[1];
                tempClip[0] = Resources.Load("ButtonClickSound_2") as AudioClip;
                return tempClip;
            }
        }
    }

    AudioSource VoiceSource => audioSource[0] = audioSource[0] ? audioSource[0] : gameObject.AddComponent<AudioSource>();
    AudioSource EffectSource => audioSource[1] = audioSource[1] ? audioSource[1] : gameObject.AddComponent<AudioSource>();
    AudioSource EnvironmentSource => audioSource[2] = audioSource[2] ? audioSource[2] : gameObject.AddComponent<AudioSource>();

    AudioSource BGMSource => bgmSource = bgmSource ? bgmSource : transform.GetChild(0).GetComponent<AudioSource>();

    #endregion

    #region Implementation Place

    #region PlayOneShot

    #region Voice
    public void PlayVoiceOneShot(AudioClip _clip, float _volume = 1)
    {
        VoiceSource.PlayOneShot(_clip, _volume * effectVolume);
    }

    public void PlayVoiceOneShot(AudioClip[] _clips, float _volume = 1)
    {
        VoiceSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume * effectVolume);
    }
    #endregion

    #region Effect
    public void PlayEffectOneShot(AudioClip _clip, float _volume = 1)
    {
        EffectSource.PlayOneShot(_clip, _volume * Instance.effectVolume);
    }

    public void PlayEffectOneShot(AudioClip[] _clips, float _volume = 1)
    {
        EffectSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume * effectVolume);
    }
    #endregion

    #region Environment
    public void PlayEnvironmentOneShot(AudioClip _clip, float _volume = 1)
    {
        EnvironmentSource.PlayOneShot(_clip, _volume * effectVolume);
    }

    public void PlayEnvironmentOneShot(AudioClip[] _clips, float _volume = 1)
    {
        EnvironmentSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume * effectVolume);
    }
    #endregion

    #endregion

    #region PlayDisConnect

    #region Voice
    public void PlayVoiceDisConnect(AudioClip _clip, float _volume = 1)
    {
        VoiceSource.Stop();
        VoiceSource.PlayOneShot(_clip, _volume);
    }

    public void PlayVoiceDisConnect(AudioClip[] _clips, float _volume = 1)
    {
        VoiceSource.Stop();
        VoiceSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume);
    }
    #endregion

    #region Effect
    public void PlayEffectDisConnect(AudioClip _clip, float _volume = 1)
    {
        EffectSource.Stop();
        EffectSource.PlayOneShot(_clip, _volume);
    }

    public void PlayEffectDisConnect(AudioClip[] _clips, float _volume = 1)
    {
        EffectSource.Stop();
        EffectSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume);
    }
    #endregion

    #region Environment
    public void PlayEnvironmentDisConnect(AudioClip _clip, float _volume = 1)
    {
        EnvironmentSource.Stop();
        EnvironmentSource.PlayOneShot(_clip, _volume);
    }

    public void PlayEnvironmentDisConnect(AudioClip[] _clips, float _volume = 1)
    {
        EnvironmentSource.Stop();
        EnvironmentSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)], _volume);
    }
    #endregion

    #endregion

    public void SetBGMVolume(float value)
    {
        BGMSource.volume = value * 0.35f;
    }

    public void SetEffectVolume(float value)
    {
        effectVolume = value;
    }

    #endregion
}
