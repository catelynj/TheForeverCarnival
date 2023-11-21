using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject dartInteractable;  // The prefab of the dart you want to add to the inventory.
    public GameObject player;
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
        GameObject dart = Instantiate(dartInteractable, inventorySlot.transform.position, Quaternion.identity);
        dart.tag = "Dart";

        // Make the dart a child of the inventory slot to ensure it moves with the player's view.
        dart.transform.parent = inventorySlot.transform;
        dart.transform.rotation = player.transform.rotation;
    }
    public void ThrowDartFromInventory()
    {
        // Check if there's a dart in the inventory slot.
        if (inventorySlot.childCount > 0)
        {
            // Get the dart GameObject.
            GameObject dart = inventorySlot.GetChild(0).gameObject;

            // Detach the dart from the inventory slot.
            dart.transform.parent = null;

            // Dart throwing logic.
            Rigidbody dartRigidbody = dart.GetComponent<Rigidbody>();
            if (dartRigidbody != null)
            {
                // Enable gravity and apply force.
                dartRigidbody.useGravity = true;
                dartRigidbody.isKinematic = false;
                dartRigidbody.AddForce(transform.forward * 15f, ForceMode.Impulse);
            }
            else
            {
                // If the Rigidbody component doesn't exist, add it and then apply force.
                Rigidbody newRigidbody = dart.AddComponent<Rigidbody>();
                newRigidbody.useGravity = true;
                newRigidbody.isKinematic = false;
                newRigidbody.AddForce(transform.forward * 15f, ForceMode.Impulse);
            }
            Destroy(dart, 5f);
        }
    }

    void Update()
    {
        // Left-click to throw dart.
        if (Input.GetMouseButtonDown(0))
        {
            ThrowDartFromInventory();
        }
    }
}
