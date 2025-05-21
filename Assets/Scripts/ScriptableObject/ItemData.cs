using UnityEngine;

public enum ItemType
{
    Equip,
    Consumable,
}

public enum ConsumableType
{
    Health_Heal,
    Stamina_Heal,
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "item",menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStack;
    
    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
    
    [Header("Equip")]
    public GameObject equipPrefab;

}
