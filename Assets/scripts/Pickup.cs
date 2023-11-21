using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isPickedUp = false;
    public GameObject dart;

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Dart") && !isPickedUp)
        {
            // Notify the player's inventory system that a dart is picked up.
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddDartToInventory();
            }
            // Destroy the dart object since it's picked up.
            //Destroy(dart);
            
        }
    }
    public void PickupItem()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;
        }
    }
}
