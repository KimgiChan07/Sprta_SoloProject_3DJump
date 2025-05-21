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
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        if(uiBar == null||maxValue<=0) return;
        if(!float.IsNaN(curValue)&&!float.IsInfinity(curValue))
            uiBar.fillAmount = GetPercentage();
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
    }

    public void Add(float _value)
    {
        curValue =Mathf.Min(curValue + _value, maxValue);
    }

    public void Subtract(float _value)
    {
        curValue =Mathf.Max(curValue - _value, 0);
    }
}
