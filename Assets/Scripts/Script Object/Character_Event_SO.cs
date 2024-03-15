using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "CharacterEvent",menuName ="Event/Character_Event")]
public class Character_Event_SO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    public void RaiseEvent(Character character)
    {
        OnEventRaised?.Invoke(character);
    }
}
