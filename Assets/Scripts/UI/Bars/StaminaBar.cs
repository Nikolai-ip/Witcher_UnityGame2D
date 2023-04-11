using UnityEngine.UI;

public class StaminaBar : Bar
{
    private void Start()
    {
        var entity = GetComponentInParent<EntityFollower>().Entity;
        slider = GetComponent<Slider>();
        maxValue = entity.MaxStamina;
        minValue = entity.MinStamina;
        slider.maxValue = maxValue;
        slider.minValue = minValue;
        entity.GetComponent<StaminaController>().OnStaminaChanged += SetSliderValue;
        entity.onDestroy += DestroyBar;
    }
}