using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            isPickedUp = true;
            // Add logic to handle the pickup (e.g., increase the player's score, disable the object, play a sound, etc.)
            // Example: player.GetComponent<PlayerController>().CollectItem();
            // Example: gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void PickupItem()
    {
        gameObject.SetActive(false);
    }

}
