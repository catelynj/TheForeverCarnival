using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowBall : MonoBehaviour
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

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Ball") && canPickup == true)
            {
                if (clone == null)
                {
                    clone = Instantiate(hit.collider.gameObject);
                    Rigidbody rb = clone.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        rb = clone.AddComponent<Rigidbody>();
                    }

                    rb.useGravity = true;
                    beingCarried = true;
                    canPickup = false; //doesnt work btw

                    //freeze player movement in mini-game
                    InputSystem.DisableDevice(Keyboard.current);
                }
            }
        }
    }


    private void Throw()
    {
        Camera playerCamera = cam.GetComponent<Camera>();
        Vector3 throwPosition = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane + cloneOffset));
        clone.transform.position = throwPosition;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 throwDirection = ray.direction;
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
        Destroy(obj);
        canPickup = true;

        //Unfreeze player movement
        InputSystem.EnableDevice(Keyboard.current);
    }
}
