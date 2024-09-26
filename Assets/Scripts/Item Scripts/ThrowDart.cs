using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowDart : MonoBehaviour
{
    public AudioSource pickupSound;
    public Transform cam;
    public RectTransform reticle;
    public float throwForce = 10f;
    public float cloneOffset = 2f;
    public float eyeLevelHeight = 1.5f;
    private GameObject clone;
    bool beingCarried = false;
    private bool canPickup = true;
    GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // sync physics for teleport
        player.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        Physics.SyncTransforms();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPickup)
            {
                Pickup();
                pickupSound.Play();
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

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Dart") && clone == null)
        {
            // Instantiate a clone only if there isn't one already
            clone = Instantiate(hit.collider.gameObject);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = clone.AddComponent<Rigidbody>();
            }

            rb.useGravity = true;
            beingCarried = true;
            canPickup = false;  // doesn't work btw
            rb.constraints = RigidbodyConstraints.None;

            // Freeze Player
            InputSystem.DisableDevice(Keyboard.current);

            //teleport player
            player.GetComponent<FirstPersonController>().enabled = false;
            player.transform.position = new Vector3(hit.transform.position.x + 1f, 0, hit.transform.position.z + 3.5f);
            player.GetComponent<FirstPersonController>().enabled = true;

            rb.useGravity = true;
            beingCarried = true;
        }
    }


    private void Throw()
    {
        Camera playerCamera = cam.GetComponent<Camera>();
        Vector3 throwPosition = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane + cloneOffset));
        clone.transform.position = throwPosition;

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 throwDirection = ray.direction;

        // Set the rotation of the clone to face the throw direction
        clone.transform.LookAt(clone.transform.position + throwDirection);

        if (Input.GetMouseButtonDown(0))
        {
            clone.transform.parent = null;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = throwDirection * throwForce;
            }

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

