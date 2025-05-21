using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;


public class InventoryUI : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;

    private PlayerCondition playerCondition;
    private PlayerController playerController;

    private ItemData selectedItem;
    private int selectedItemIndex;

    private void Start()
    {
        playerCondition = CharacterManager.Instance.Player.playerCondition;
        playerController = CharacterManager.Instance.Player.playerController;
        CharacterManager.Instance.Player.addItem += AddItem;

        slots = new ItemSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventoryUI = this;
        }

        slots[0].SetSelected(true);
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            MoveSelection(1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            MoveSelection(-1);
        }
    }

    public void AddItem()
    {
        ItemData item = CharacterManager.Instance.Player.itemData;

        if (item.canStack)
        {
            ItemSlot slot = GetItemStack(item);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }

            CharacterManager.Instance.Player.itemData = null;
        }

        ItemSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }


        CharacterManager.Instance.Player.itemData = null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }

            slots[i].SetSelected(i == selectedItemIndex);
        }
    }

    ItemSlot GetItemStack(ItemData _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == _item && slots[i].quantity < _item.maxStack)
            {
                return slots[i];
            }
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }

        return null;
    }

    void SelectItem(int _index)
    {
        if (slots[_index] == null) return;

        selectedItem = slots[_index].item;
        selectedItemIndex = _index;
    }

    private void MoveSelection(int _delta)
    {
        int prev = selectedItemIndex;
        selectedItemIndex += _delta;
        selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, slots.Length - 1);

        UpdateSelectedItemOutline(prev, selectedItemIndex);
        SelectItem(selectedItemIndex);
    }

    void UpdateSelectedItemOutline(int _prev, int _next)
    {
        if (slots[_prev] != null)
        {
            slots[_prev].SetSelected(false);
        }

        if (slots[_next] != null)
        {
            slots[_next].SetSelected(true);
        }
    }

    public void OnUseSelectedItem(InputAction.CallbackContext context)
    {
        if (selectedItemIndex < 0 || selectedItemIndex >= slots.Length) return;

        if (context.phase == InputActionPhase.Started)
            UseItem(selectedItemIndex);
    }

    void UseItem(int _index)
    {
        if (slots[_index] == null || _index < 0 || _index >= slots.Length) return;

        var item = slots[_index];

        if (item == null)
        {
            return;
        }

        if (item.item == null)
        {
            return;
        }

        if (playerCondition == null)
        {
            return;
        }

        if (item.item.type != ItemType.Consumable)
        {
            return;
        }

        foreach (var itemEffect in item.item.consumables)
        {
            switch (itemEffect.type)
            {
                case ConsumableType.Health_Heal:
                    playerCondition.HealthHeal(20);
                    break;
            }
        }

        slots[_index].quantity--;
        if (slots[_index].quantity <= 0)
        {
            slots[_index].item = null;
        }

        UpdateUI();
    }
}