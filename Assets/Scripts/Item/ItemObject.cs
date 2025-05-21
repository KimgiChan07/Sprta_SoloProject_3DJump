using UnityEngine;

public interface Iinteractable
{
    public string GetDescription();
    public void OnInteract();
}

public class ItemObject : MonoBehaviour,  Iinteractable
{
    public ItemData itemData;
    public string GetDescription()
    {
        string str = $"[{itemData.displayName}]\n{itemData.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}