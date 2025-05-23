using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCondition playerCondition;
    
    public ItemData itemData;
    public Action addItem;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        playerCondition=GetComponent<PlayerCondition>();
        playerController=GetComponent<PlayerController>();
    }
}
