//Name: 
//Date: 
//Purpose: Let the player respawn the balloons by pressing 'E' on a button
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bloonerRespawn : MonoBehaviour
{

    public int balloons;
    public GameObject balloon;
    bool canButton = true;
    public GameObject spawnArea;
    IEnumerator balloonRespawner;
    public float waitTime;

    private void Start()
    {
        //balloonRespawner = BalloonSpawner();
        StartCoroutine(BalloonSpawner());
        // Call the spawning function when the game begins
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canButton)
        {
                respawnBloons();
        }
    }

    void respawnBloons()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("dartButton"))
        {
                Debug.Log("Button pressed!");
                StartCoroutine(BalloonSpawner());
        }
    }

    // BUG: Coroutine is only being called once and then skipped over. Not sure why.
    IEnumerator BalloonSpawner()
    {
        canButton = false;
        Collider spawn = spawnArea.GetComponent<BoxCollider>();
        Rigidbody rigi = balloon.GetComponent<Rigidbody>();
        for (int i = 0; i < balloons; i++)
        {
            // Generate a random position within the BoxCollider bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(spawn.bounds.min.x, spawn.bounds.max.x),
                Random.Range(spawn.bounds.min.y, spawn.bounds.max.y),
                Random.Range(spawn.bounds.min.z, spawn.bounds.max.z)
            );

            Instantiate(balloon, randomPosition, Quaternion.identity); // Spawn balloon at random position
        }
        Debug.Log("Waiting..." + waitTime);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ready!" + waitTime);
        canButton = true;
    }

}
