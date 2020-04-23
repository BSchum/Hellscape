using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossAudioManager : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip claw;
    public AudioClip clawCast;
    public AudioClip chained;
    public AudioClip charging;
    public AudioClip lavaSpit;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #region Called by Animation Event
    /// <summary>
    /// Called by ClawCast animation
    /// </summary>
    public void PlayClawCast()
    {
        _audioSource.clip = clawCast;
        _audioSource.Play();
    }

    /// <summary>
    /// Called by Claw Animation
    /// </summary>
    public void PlayClaw()
    {
        _audioSource.clip = claw;
        _audioSource.Play();
    }
    #endregion
}