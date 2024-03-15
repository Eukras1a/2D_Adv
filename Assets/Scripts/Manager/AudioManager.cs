using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource FXSource;

    public Audio_Event_SO BGMAudioEvent;
    public Audio_Event_SO FXAudioEvent;

    private void OnEnable()
    {
        BGMAudioEvent.OnEventRaised += OnBGMEvent;
        FXAudioEvent.OnEventRaised += OnFXEvent;
    }
    private void OnDisable()
    {
        BGMAudioEvent.OnEventRaised -= OnBGMEvent;
        FXAudioEvent.OnEventRaised -= OnFXEvent;
    }
    private void OnBGMEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXEvent(AudioClip clip)
    {
        FXSource.PlayOneShot(clip);
    }
}
