//Name: 
//Date: 
//Purpose: Generate a set amount of balloons for the player to pop
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blooner : MonoBehaviour
{ }/*
    public int balloons;
    public GameObject balloon;

    // Start is called before the first frame update
    void Start()
    {
        balloonSpawner(); // Call the spawning function when the game begins
    }

    public void balloonSpawner()
    {
        Collider spawnArea = GetComponentInParent<BoxCollider>();
        Rigidbody rigi = balloon.GetComponent<Rigidbody>();
        for (int i = 0; balloons > i; i++)
        {
            //TODO: SPAWN BALLOONS IN RANDOM AREAS ALONG THE BOX COLLIDER
            Instantiate(balloon, spawnArea.transform);
            
            // BUG:
            // When spawning more than one balloon, they can spawn inside of each other and eject out into space
            rigi.constraints = RigidbodyConstraints.FreezePosition; // 
            rigi.constraints = RigidbodyConstraints.FreezeRotation; // 
        }
    }

}
*/