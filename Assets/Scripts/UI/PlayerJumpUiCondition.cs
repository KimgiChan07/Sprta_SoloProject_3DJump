using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpUiCondition : MonoBehaviour
{
    public Image gaugeImage;

    private void Awake()
    {
        gaugeImage.fillAmount = 0f;
    }

    public void Show()
    {
        gaugeImage.fillAmount = 0f;
        gaugeImage.gameObject.SetActive(true);
    }

    public void SetRatio(float _value)
    {
        _value = Mathf.Clamp01(_value);

        if (float.IsNaN(_value) || float.IsInfinity(_value))
        {
            return;
        }
        gaugeImage.fillAmount = _value;
    }
}
