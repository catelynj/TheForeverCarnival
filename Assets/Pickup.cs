using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isDart = false; // Set this to true if the object is a dart.
    public bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isDart)
            {
                // Notify the player's inventory system that a dart is picked up.
                Inventory inventory = other.GetComponent<Inventory>();
                if (inventory != null)
                {
                    inventory.AddDartToInventory();
                }
                // Optionally, destroy the dart object since it's picked up.
                Destroy(gameObject);
            }
            // Handle other types of pickups or interactions here.
        }
    }
    public void PickupItem()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
            // Add logic to handle the pickup (e.g., increase the player's score, disable the object, play a sound, etc.)
            // Example: player.GetComponent<PlayerController>().CollectItem();
            // Example: gameObject.SetActive(false);
            //gameObject.SetActive(false);

        }
    }
}

