using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossAudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    AudioClip chained;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayChained()
    {
        _audioSource.clip = chained;
        _audioSource.Play();
    }
}