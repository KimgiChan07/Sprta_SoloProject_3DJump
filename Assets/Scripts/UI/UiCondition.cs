using System;
using UnityEngine;

public class UiCondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Start()
    {
        CharacterManager.Instance.Player.playerCondition.uiCondition = this;
    }
}
