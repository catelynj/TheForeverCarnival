using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class ShootGun : MonoBehaviour
{
    public AudioClip pickupSound;
    private AudioSource pickupSource;
    public Transform cam;
    public RectTransform reticle;
    public float shootForce = 10f;
    public float cloneOffset = 2f;
    public float eyeLevelHeight = 1.5f;
    private GameObject clone;
    bool beingCarried = false;
    private bool canPickup = true;
    GameObject player;
    bool keyboardActive = true;

    public static ShootGun SharedInstance;
    public GameObject bullet;
    GameObject bulletclone;
    int bulletcounter;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupSource = GetComponent<AudioSource>();

        // sync physics for teleport
        player.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        Physics.SyncTransforms();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPickup)
        {
            Pickup();
        }

        if (beingCarried && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Pickup()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Gun");
        int gunclones = 0;
        foreach (GameObject gun in gameObjects) { gunclones++; }
        if (beingCarried || gunclones >= 2) return; // This should stop us from picking up multiple guns
        gameObjects = null;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Gun") && clone == null)
        {
            if (keyboardActive)
            {
                //freeze player movement in mini-game
                InputSystem.DisableDevice(Keyboard.current);
                keyboardActive = false;

                // teleport player
                player.GetComponent<FirstPersonController>().enabled = false;
                player.transform.position = new Vector3(hit.transform.position.x + 0.5f, 0, hit.transform.position.z+1);
                player.GetComponent<FirstPersonController>().enabled = true;

            }

            // Instantiate a clone only if there isn't one already
            clone = Instantiate(hit.collider.gameObject);
            bulletcounter = 5;

            // gun model
            clone.transform.position = new Vector3(player.transform.position.x - 0.2f, player.transform.position.y + 1.6f, player.transform.position.z + 0.1f);
            clone.transform.Rotate(180f, 0f, 90f);
            clone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // audioSource.PlayOneShot(pickupSound);

            beingCarried = true;
            canPickup = false;  // doesn't work btw

            beingCarried = true;

            //pickup sound
            if (pickupSound != null && pickupSource != null)
            {
                pickupSource.PlayOneShot(pickupSound);
            }
        }
    }

    private void Shoot()
    {
        Camera playerCamera = cam.GetComponent<Camera>();
        Vector3 throwPosition = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCamera.nearClipPlane + cloneOffset));

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 throwDirection = ray.direction;

        if (Input.GetMouseButtonDown(0) && bulletcounter > 0)
        {

            if (bullet != null)
            {
                
                bulletclone = Instantiate(bullet);
                bulletclone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, clone.transform.position.z - 1);
                bulletclone.transform.rotation = clone.transform.rotation;
                bulletclone.transform.LookAt(bullet.transform.position + throwDirection);
                Rigidbody rb = bulletclone.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = throwDirection * shootForce;
                }
            }
            
            StartCoroutine(DestroyAfterDelay(bulletclone, 1f));
            bulletcounter--;
        }

    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy the object after the delay
        Destroy(obj);
        canPickup = true;  // Allow picking up a new object after the current one is destroyed
        if(bulletcounter == 0)
        {
            // Unfreeze Player
            InputSystem.EnableDevice(Keyboard.current);
            keyboardActive = true;
            Destroy(clone);
            beingCarried = false;
        }

    }

}
