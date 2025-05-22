using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public Image uiBar;

    private void Start()
    {
        Set(startValue);
    }
    float GetPercentage()
    {
        float percentage = curValue/maxValue;
        percentage=Mathf.Clamp01(percentage);
        return percentage;
    }

    public void Set(float _value)
    {
        curValue=Mathf.Clamp(_value,0f,maxValue);
        UpdateUI();
    }

    public void Add(float _value)
    {
        Set(curValue + _value);
    }

    public void Subtract(float _value)
    {
        Set(curValue - _value);
    }

    void UpdateUI()
    {
        if (uiBar != null)
        {
            uiBar.fillAmount = GetPercentage();
        }
    }
}
