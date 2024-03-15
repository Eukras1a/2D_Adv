using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;

    private void Update()
    {
        UpdateHealthDelayImage();
    }
    public void OnHealthChange(float presentage)
    {
        healthImage.fillAmount = presentage;
    }
    void UpdateHealthDelayImage()
    {
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime*0.5f;
        }
    }
}
