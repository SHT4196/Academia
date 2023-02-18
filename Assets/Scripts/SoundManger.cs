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

    public AudioSource EffectAudio;
    public AudioSource EndingAudio;

    public AudioClip ClickSound;
    public AudioClip PlusSound;
    public AudioClip MinusSound;
    public AudioClip ChangingStatSound;
    public AudioClip GoodEndingBGM;
    public AudioClip BadEndingBGM;

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
            float bgmSound = PlayerPrefs.GetFloat("BGMvolume");
            BgmSlider.value = bgmSound;
            masterMixer.SetFloat("BGM", bgmSound);
        }
        if (PlayerPrefs.HasKey("Effectvolume"))
        {
            float effectSound = PlayerPrefs.GetFloat("Effectvolume");
            EffectSlider.value = effectSound;
            masterMixer.SetFloat("Effect", effectSound);
        }
    }

    public void BGMVolumeUp()
    {
        BgmSlider.value += 5;
        float sound = BgmSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else {
            masterMixer.SetFloat("BGM", sound);
            PlayerPrefs.SetFloat("BGMvolume", sound);
        }
    }

    public void BGMVolumeDown()
    {
        BgmSlider.value -= 5;
        float sound = BgmSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else{
            masterMixer.SetFloat("BGM", sound);
            PlayerPrefs.SetFloat("BGMvolume", sound);
        }
    }

    public void EffectVolumeUp()
    {
        EffectSlider.value += 5;
        float sound = EffectSlider.value;

        if (sound == -40f) masterMixer.SetFloat("Effect", -80);
        else{
            masterMixer.SetFloat("Effect", sound);
            PlayerPrefs.SetFloat("EffectVolume", sound);
        }
    }

    public void EffectVolumeDown()
    {
        EffectSlider.value -= 5;
        float sound = EffectSlider.value;

        if (sound == -40f) masterMixer.SetFloat("Effect", -80);
        else{
            masterMixer.SetFloat("Effect", sound);
            PlayerPrefs.SetFloat("EffectVolume", sound);
        }
    }

    public void ResestSoundSettings()
    {
        //reset sound values
        masterMixer.SetFloat("BGM", -40);
        masterMixer.SetFloat("Effect", -40);
        BgmSlider.value = -20;
        EffectSlider.value = -20;
    }

    public void PlayBtnSound()
    {
        EffectAudio.PlayOneShot(ClickSound);
    }

    public void stopAudio()
    {
        // audio = GetComponenet<AudioSource>();
        // audio.Stop();
    }

    public void PlayPlusSound()
    {
        EffectAudio.PlayOneShot(PlusSound);
    }

    public void PlayMinusSound()
    {
        EffectAudio.PlayOneShot(MinusSound);
    }

    public void PlayChangingStatSound()
    {
        EffectAudio.PlayOneShot(ChangingStatSound);
    }

    //play good ending bgm
    public void PlayGoodEndingBGM()
    {
        EndingAudio.PlayOneShot(GoodEndingBGM);
    }

    //play bad ending bgm
    public void PlayBadEndingBGM()
    {
        EndingAudio.PlayOneShot(BadEndingBGM);
    }

    //stop ending audio (at good and bad)
    public void StopEndingAudio()
    {
        EndingAudio.Stop();
    }

}