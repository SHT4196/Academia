using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{

    public AudioMixer masterMixer;
    public Slider BgmSlider;
    public Slider EffectSlider;

    public AudioSource audioSource;
    public AudioClip ClickSound;
    public static SoundManger instance;

    /// <summary>
    /// ���� ����
    /// </summary>

    void Awake()
    {
        if (SoundManger.instance == null)
        {
            SoundManger.instance = this;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("BGMvolume"))
        {
            BgmSlider.value = PlayerPrefs.GetFloat("BGMvolume");
            // masterMixer.setFloat("BGM", BgmSlider.value);
        }
        if (PlayerPrefs.HasKey("Effectvolume"))
        {
            BgmSlider.value = PlayerPrefs.GetFloat("Effectvolume");
            // masterMixer.setFloat("BGM", BgmSlider.value);
        }
    }

    public void BGMAudioControl()
    {
        float sound = BgmSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else
        {
            masterMixer.SetFloat("BGM", sound);
            PlayerPrefs.SetFloat("BGMvolume", sound);
        }
    }
    public void EffectAudioControl()
    {
        float sound = EffectSlider.value;

        if (sound == -40f) masterMixer.SetFloat("Effect", -80);
        else
        {
            masterMixer.SetFloat("Effect", sound);
            PlayerPrefs.SetFloat("EffectVolume", sound);
        }
    }

    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(ClickSound);
    }

}