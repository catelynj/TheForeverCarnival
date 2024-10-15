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
    bool beingCarried = false;
    private bool canPickup = true;
    GameObject player;
    GameObject bball;

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
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("basketball"))
        {
            beingCarried = true;
            bball = hit.collider.gameObject;
        }
    }

    void Throw()
    {
        Camera playerCamera = cam.GetComponent<Camera>();
        Vector3 throwPosition = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane + cloneOffset));
        bball.transform.position = throwPosition;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 throwDirection = ray.direction;
        bball.transform.LookAt(bball.transform.position + throwDirection);
        if (Input.GetMouseButtonDown(0))
        {
            bball.transform.parent = null;
            Rigidbody rb = bball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = throwDirection * throwForce;
            }
            beingCarried = false;
            canPickup = true;
        }
    }

}
