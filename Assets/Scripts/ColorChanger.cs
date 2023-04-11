using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float brightnessValue;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        float brightness = currentColor.maxColorComponent;
        Color newColor = currentColor * (1 + (brightnessValue / brightness));
        spriteRenderer.color = newColor;
    }
}
