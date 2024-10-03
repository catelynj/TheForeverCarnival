using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowBall : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public Transform cam;
    public RectTransform reticle;
    public float throwForce = 10f;
    public float cloneOffset = 2f;
    public float eyeLevelHeight = 1.5f;
    private GameObject clone;
    bool beingCarried = false;
    private bool canPickup = true;
    GameObject player;
    bool keyboardActive = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

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
            }
        }

        if (beingCarried)
        {
            Throw();
        }
    }

    private void Pickup()
    {

        /*GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Ball");
        int ballclonse = 0;
        foreach (GameObject ball in gameObjects) { ballclonse++; Debug.Log(ballclonse); }
        if (beingCarried || ballclonse >= 2) return; // This should stop us from picking up multiple darts
        gameObjects = null;*/

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Ball") && clone == null)
        {
            if (keyboardActive == true)
            {
                //freeze player movement in mini-game
                InputSystem.DisableDevice(Keyboard.current);
                keyboardActive = false;

                //teleport player
                player.GetComponent<FirstPersonController>().enabled = false;
                player.transform.position = new Vector3(hit.transform.position.x + 1f, 0, hit.transform.position.z);
                player.GetComponent<FirstPersonController>().enabled = true;
            }

            //ball clone instantiation
            clone = Instantiate(hit.collider.gameObject);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = clone.AddComponent<Rigidbody>();
            }

            rb.useGravity = true;
            beingCarried = true;
            rb.constraints = RigidbodyConstraints.None;

            //pickup sound
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
                canPickup = false;
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
            canPickup = true;
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);

        //Unfreeze player movement
        InputSystem.EnableDevice(Keyboard.current);
        keyboardActive = true;
    }
}
