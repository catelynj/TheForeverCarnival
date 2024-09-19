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

    private void Start()
    {
        balloonSpawner(); // Call the spawning function when the game begins
        //blooner other = spawnArea.GetComponent<blooner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canButton)
            {
                respawnBloons();
            }
        }
    }

    void respawnBloons()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("dartButton"))
            {
                balloonSpawner();
                //blooner blnr = new blooner();
                //blnr.balloonSpawner(); //Need to find a way to implement this
            }
        }
    }

    void balloonSpawner()
    {
        Collider spawn = spawnArea.GetComponent<BoxCollider>();
        Rigidbody rigi = balloon.GetComponent<Rigidbody>();
        for (int i = 0; balloons > i; i++)
        {
            //TODO: SPAWN BALLOONS IN RANDOM AREAS ALONG THE BOX COLLIDER
            Instantiate(balloon, spawn.transform);

            // BUG:
            // When spawning more than one balloon, they can spawn inside of each other and eject out into space
            rigi.constraints = RigidbodyConstraints.FreezePosition; // 
            rigi.constraints = RigidbodyConstraints.FreezeRotation; // 
        }
    }

}
