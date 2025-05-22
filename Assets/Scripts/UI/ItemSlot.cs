using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    

    [Header("UI")] 
    public Image icon;
    public TextMeshProUGUI quantityText;
    public Outline outline;
    public InventoryUI inventoryUI;

    public int index;
    public int quantity;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void Set()
    {
        if(item==null)return;
        if(icon==null)return;
        
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quantityText.text = quantity >= 0 ? quantity.ToString() : String.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        quantity = 0;
        icon.sprite = null;
        quantityText.text = quantity.ToString();
        if(outline != null)
            outline.enabled = false;
    }

    public void SetSelected(bool _isSelected)
    {
        if(outline == null)return;
        outline.enabled = _isSelected;
    }

}