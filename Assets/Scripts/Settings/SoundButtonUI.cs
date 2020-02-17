using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundButtonUI : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    public AudioMixer audioMixer;
    public Slider slider;

    public void SetVolume()
    {
        audioMixer.SetFloat(mixerGroup.name, slider.value);
    }
}
