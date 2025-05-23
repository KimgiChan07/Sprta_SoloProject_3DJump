using System;
using UnityEngine;

public interface IDamageIbe
{
    void TakePhysicalDamage(float damage);
}

public class PlayerCondition : MonoBehaviour, IDamageIbe
{
    public UiCondition uiCondition;

    public Condition health { get { return uiCondition.health; } }
    
    public Condition stamina { get { return uiCondition.stamina; } }
    
    [Header("Jump")] public float minJumpForce;
    public float maxJumpForce;
    public float maxChargeTime;
    private float jumpStaminaCost;
    private float jumpCharge;
    public bool isCharging { get; private set; }

    public event Action onTakeDamage;

    private void Update()
    {
        if (isCharging)
        {
            jumpCharge += Time.deltaTime;
            PlayerJumpUiCondition ui = FindAnyObjectByType<PlayerJumpUiCondition>();
            if (ui != null)
            {
                ui.SetRatio(jumpCharge / maxChargeTime);
            }
        }
    }

    //-----------------------Jump------------------------
    public bool IsJumpCharge()
    {
        isCharging = true;
        jumpCharge = 0f;
        return true;
    }

    public float EndJumpCharge()
    {
        isCharging = false;
        float ratio = Mathf.Clamp01(jumpCharge / maxChargeTime);
        float force = Mathf.Lerp(minJumpForce, maxJumpForce, ratio);

        stamina.curValue = 0f;
        jumpCharge = 0f;

        return force;
    }

    //-----------------------Stat------------------------
    public void HealthHeal(float _amount)
    {
        health.Add(_amount);
    }

    public void TakePhysicalDamage(float damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }
}