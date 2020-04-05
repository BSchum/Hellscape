using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundButtonUI : MonoBehaviour
{
    public SoundSettingController soundSettingController;

    public SoundSettingController.SoundGroup group;

    public AudioMixerGroup mixerGroup;
    public AudioMixer audioMixer;
    public Slider slider;

    public void SetVolume()
    {
        audioMixer.SetFloat(mixerGroup.name, slider.value);

        soundSettingController.SetVolume(slider.value, group);
    }
}
