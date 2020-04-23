using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<SceneAudio> sceneAudios;

    private static AudioManager _currentAudioManager;

    private void Start()
    {
        if(_currentAudioManager == null)
        {
            _currentAudioManager = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(mode != LoadSceneMode.Additive)
        {
            var currentAudio = sceneAudios.Where(sa => sa.ScenesNames.Contains(scene.name)).FirstOrDefault().audio;
            if (audioSource.clip.name != currentAudio.name)
            {
                audioSource.clip = currentAudio;
            }
        }
    }
}
