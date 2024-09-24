using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowBall : MonoBehaviour
{
    public Transform cam;
    public RectTransform reticle;
    public float throwForce = 10f;
    public float cloneOffset = 2f;
    public float eyeLevelHeight = 1.5f;
    private GameObject clone;
    bool beingCarried = false;
    private bool canPickup = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPickup)
            {
                Pickup();
            }
        }

        if (beingCarried)
        {
            Throw();
        }
    }

    private void Pickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ball") && canPickup == true)
            {
                // Instantiate a clone only if there isn't one already
                if (clone == null)
                {
                    clone = Instantiate(hit.collider.gameObject);

                    // Disable the clone's collider while being carried to prevent interference
                    Collider cloneCollider = clone.GetComponent<Collider>();
                    if (cloneCollider != null)
                    {
                        cloneCollider.enabled = false;
                    }

                    // Make sure the clone has a Rigidbody
                    Rigidbody rb = clone.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        rb = clone.AddComponent<Rigidbody>();
                    }

                    // Enable gravity for the Rigidbody
                    rb.useGravity = true;

                    beingCarried = true;
                    
                    canPickup = false;  // Set to false to prevent rapid pickups
                    // Freeze Player
                    InputSystem.DisableDevice(Keyboard.current);
                }
            }
        }
    }


    private void Throw()
    {
        // Get the Camera component from the playerCam Camera
        Camera playerCamera = cam.GetComponent<Camera>();

        // Use ViewportToWorldPoint to get a position in world space based on the center of the screen
        Vector3 throwPosition = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane + cloneOffset));

        // Set the position of the clone
        clone.transform.position = throwPosition;

        // Use the center of the screen as the target position
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 throwDirection = ray.direction;

        // Set the rotation of the clone to face the throw direction
        clone.transform.LookAt(clone.transform.position + throwDirection);

        if (Input.GetMouseButtonDown(0))
        {
            // Enable the clone's collider when thrown
            Collider cloneCollider = clone.GetComponent<Collider>();
            if (cloneCollider != null)
            {
                cloneCollider.enabled = true;
            }

            // Detach the clone from the player's camera
            clone.transform.parent = null;

            // Apply a constant force rather than using AddForce
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = throwDirection * throwForce;
            }

            // Start a coroutine to despawn the clone after a delay
            StartCoroutine(DestroyAfterDelay(clone, 0.5f)); 
            beingCarried = false;
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy the object after the delay
        Destroy(obj);
        canPickup = true;  // Allow picking up a new object after the current one is destroyed
        // Unfreeze Player
        InputSystem.EnableDevice(Keyboard.current);
    }
}
