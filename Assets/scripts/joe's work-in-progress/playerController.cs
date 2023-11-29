using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LockCursor();

    }

    private void Update()
    {
        // Basic movement controls
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize(); // Normalize to prevent faster diagonal movement
        movement *= moveSpeed * Time.deltaTime;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Interaction with objects (e.g., pickup items)
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract(); // You can define this function to handle interactions
        }
    }

    private void TryInteract()
    {
        // Check for nearby objects that can be interacted with (e.g., pickup items)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            // Check if the hit object has a "Pickup" script attached
            Pickup pickup = hit.collider.GetComponent<Pickup>();
            if (pickup != null && !pickup.isPickedUp)
            {
                // Call the Pickup function in the Pickup script
                pickup.PickupItem();
            }
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState -= CursorLockMode.None;
    }

}
