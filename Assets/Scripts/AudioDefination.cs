using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    public Audio_Event_SO audioEvent;
    public AudioClip audioClip;
    public bool playOnEnable;
    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudioClip();
        }
    }
    void PlayAudioClip()
    {
        audioEvent.RaiseEvent(audioClip);
    }
}
