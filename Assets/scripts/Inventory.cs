using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] inventory;

 

    private void Start()
    {
        InitVariables();
               
    }

    private void Update()
    {
    }
    public void AddItem(Item newItem)
    {
        if (inventory[(int)newItem.itemType] != null)
        {
            RemoveItem((int)newItem.itemType);
        }
        inventory[(int)newItem.itemType] = newItem;
    }

    private void RemoveItem(int index)
    {
        inventory[index] = null;
    }
    private void InitVariables()
    {
        inventory = new Item[3];
    }
}
