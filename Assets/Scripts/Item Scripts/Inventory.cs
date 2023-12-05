using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Item2[] inventory;

    private UIManager hud;

    private void Start()
    {
        hud = GetComponent<UIManager>();
        InitVariables();
               
    }
    public void AddItem(Item2 newItem)
    {
        if (inventory[(int)newItem.itemType] != null)
        {
            RemoveItem((int)newItem.itemType);
        }
        inventory[(int)newItem.itemType] = newItem;

        //Update Inventory UI
        hud.UpdateInventoryUI(newItem);
    }
    public Item2 GetItem(int index)
    {
        return inventory[index];
    }
    private void RemoveItem(int index)
    {
        inventory[index] = null;
    }
    private void InitVariables()
    {
        inventory = new Item2[2];
    }
}
