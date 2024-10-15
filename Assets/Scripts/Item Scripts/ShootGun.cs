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
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

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

        // pool stuff
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
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
        foreach (GameObject gun in gameObjects) { gunclones++; /*Debug.Log(dartclonse);*/ }
        if (beingCarried || gunclones >= 2) return; // This should stop us from picking up multiple darts
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

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = ShootGun.SharedInstance.GetPooledObject();
            if(bullet != null)
            {
                bullet.transform.position = player.transform.position;
                bullet.transform.rotation = player.transform.rotation;
                bullet.SetActive(true);
                bullet.transform.LookAt(bullet.transform.position + throwDirection);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = throwDirection * shootForce;
                }
            }
            clone.transform.parent = null;
            
            
            StartCoroutine(DestroyAfterDelay(clone, 0.5f));
            beingCarried = false;
        }

    }

    private IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy the object after the delay
        Destroy(obj);
        clone = null;
        canPickup = true;  // Allow picking up a new object after the current one is destroyed
        // Unfreeze Player
        InputSystem.EnableDevice(Keyboard.current);
        keyboardActive = true;
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
