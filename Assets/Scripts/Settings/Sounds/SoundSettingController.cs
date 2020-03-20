using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingController : MonoBehaviour
{
    private SoundData soundData;

    public SoundButtonUI[] soundButtons;

    public enum SoundGroup { Master, Ambient, Fx }

    private void Awake()
    {
        soundData = SaveSystem.LoadData<SoundData>(SaveSystem.Data.Sounds);
        if (soundData == null)
        {
            soundData = new SoundData();
        }
    }

    private void OnEnable()
    {
        foreach(SoundButtonUI soundButton in soundButtons)
        {
            switch (soundButton.group)
            {
                case SoundGroup.Master:
                    soundButton.slider.value = soundData.master;
                    break;

                case SoundGroup.Ambient:
                    soundButton.slider.value = soundData.ambient;
                    break;

                case SoundGroup.Fx:
                    soundButton.slider.value = soundData.fx;
                    break;

                default:
                    break;
            }

            soundButton.SetVolume();
        }
    }

    public void SetVolume(float volume, SoundGroup group)
    {
        switch(group)
        {
            case SoundGroup.Master:
                soundData.master = volume;
                break;

            case SoundGroup.Ambient:
                soundData.ambient = volume;
                break;

            case SoundGroup.Fx:
                soundData.fx = volume;
                break;

            default:
                break;
        }
        
        Save();
    }

    private void Save()
    {
        SaveSystem.SaveData(soundData, SaveSystem.Data.Sounds);
    }
}
