using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject dartInteractable;  // The prefab of the dart you want to add to the inventory.
    private Transform inventorySlot; // The position where the dart will be displayed in the player's field of view.

    private void Start()
    {
        // Find the inventory slot (e.g., a UI element) or create an empty GameObject to serve as the inventory slot.
        // This should be the position where the dart will appear in the player's field of view.
        // For this example, let's assume you have an empty GameObject called "InventorySlot" at the bottom right of the player's screen.
        inventorySlot = GameObject.Find("inventorySlot").transform;
    }

    public void AddDartToInventory()
    {
        // Instantiate the dart prefab at the inventory slot position.
        GameObject dart = Instantiate(dartInteractable, inventorySlot.position, Quaternion.identity);

        // Make the dart a child of the inventory slot to ensure it moves with the player's view.
        dart.transform.parent = inventorySlot;
    }
}
