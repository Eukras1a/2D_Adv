using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioEvent",menuName = "Event/Audio_Event")]
public class Audio_Event_SO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;
    public void RaiseEvent(AudioClip clip)
    {
        OnEventRaised?.Invoke(clip);
    }
}
