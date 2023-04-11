using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    protected Slider slider;
    protected float maxValue;
    protected float minValue;
    [SerializeField] protected float setValueDelay;
    protected void SetSliderValue(float value)
    {
        slider.value = value;
    }
    protected void SmoothSetSliderValue(float value)
    {
        StopAllCoroutines();
        StartCoroutine(SmothSetValue(value));
    }
    protected IEnumerator SmothSetValue(float value)
    {
        float time = 0;
        float originSliderValue = slider.value;
        float dValue = value - originSliderValue;
        while (Compare(slider.value, value, Mathf.Sign(dValue)))
        {
            time += Time.deltaTime;
            slider.value += time * (dValue / setValueDelay);
            yield return null;
        }
        slider.value = value;
    }
    protected bool Compare(float sliderValue, float setValue, float sign)
    {
        if (sign == 1)
        {
            return MathF.Round(slider.value, 1) < MathF.Round(setValue, 1);
        }
        return MathF.Round(slider.value, 1) > MathF.Round(setValue, 1);
    }
    protected void DestroyBar()
    {
        Destroy(transform.parent.gameObject);
    }
}