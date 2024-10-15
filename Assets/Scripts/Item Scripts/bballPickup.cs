using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bballPickup : MonoBehaviour
{
    public Transform cam;
    public RectTransform reticle;
    public float throwForce = 10f;
    public float cloneOffset = 2f;
    public float eyeLevelHeight = 1.5f;
    private GameObject clone;
    bool beingCarried = false;
    private bool canPickup = true;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
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

    void Pickup()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        Vector3 vector3 = new Vector3(0f,-10f,0f);
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("basketball"))// && GameObject.Find("basketball(Clone)") == null)
        {
            clone = Instantiate(hit.collider.gameObject);
            hit.collider.gameObject.transform.position = vector3;
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = clone.AddComponent<Rigidbody>();
            }

            rb.useGravity = true;
            beingCarried = true;
            rb.constraints = RigidbodyConstraints.None;

        }

    }

    void Throw()
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
            beingCarried = false;
            canPickup = true;
        }
    }

}
