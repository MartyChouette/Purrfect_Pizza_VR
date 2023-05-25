using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color inactiveColor;
    
    [HideInInspector] public float lerpColorTDelta;
    [HideInInspector] public bool isActive;

    private Color activeColor;
    private float lerpColorT;

    private void Start()
    {
        activeColor = fillImage.color;
        lerpColorT = 0f;
        isActive = true;
    }

    public float sliderValue
    {
        set
        {
            slider.value = value;
        }
    }

    public void setFillColor()
    {
        lerpColorT = Mathf.MoveTowards(lerpColorT, 1, lerpColorTDelta * Time.deltaTime);
        if (lerpColorT >= 1 )
        {
            isActive = false;
        }
        fillImage.color = Color.Lerp(activeColor, inactiveColor, lerpColorT);
    }

    public void resetActiveness()
    {
        fillImage.color = activeColor;
        lerpColorT = 0;
        isActive = true;
    }
}
