using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public Character_Event_SO healthEvent;

    public PlayerUI playerUI;
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = (float)character.currentHealth / (float)character.maxHealth;
        playerUI.OnHealthChange(persentage);
    }

    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }
}
