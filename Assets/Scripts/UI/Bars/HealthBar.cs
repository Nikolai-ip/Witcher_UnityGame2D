using UnityEngine.UI;

public class HealthBar : Bar
{
    private void Start()
    {
        var entity = GetComponentInParent<EntityFollower>().Entity;
        slider = GetComponent<Slider>();
        maxValue = entity.Health;
        slider.maxValue = maxValue;
        slider.minValue = 0;
        slider.value = maxValue;
        entity.onHealthchanged += SmoothSetSliderValue;
        entity.onDestroy += DestroyBar;
    }
}