using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;
    
    private Coroutine coroutine;
    void Start()
    {
        CharacterManager.Instance.Player.playerCondition.onTakeDamage += Flash;
    }

    void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        image.enabled = true;
        image.color = new Color(1f, 100f / 255f, 100f / 255);
        coroutine = StartCoroutine(FadeWay());
    }

    IEnumerator FadeWay()
    {
        float startAlpha = 0.3f;
        float a= startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed * Time.deltaTime);
            image.color = new Color(1f, 100f / 255f, 100f / 255, a);
            yield return null;
        }   
        
        image.enabled = false;
    }
}
