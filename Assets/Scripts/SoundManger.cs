using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{

    
    public AudioMixer masterMixer;
    public Slider BgmSlider;

    /// <summary>
    /// ���� ����
    /// </summary>

    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            BgmSlider.value = PlayerPrefs.GetFloat("volume");
        }
    }

    public void AudioControl()
    {
        float sound = BgmSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else
        {
            masterMixer.SetFloat("BGM", sound);
            PlayerPrefs.SetFloat("volume", sound);
        }
    }


}