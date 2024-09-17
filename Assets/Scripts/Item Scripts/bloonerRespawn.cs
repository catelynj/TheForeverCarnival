//Name: 
//Date: 
//Purpose: Let the player respawn the balloons by pressing 'E' on a button
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloonerRespawn : MonoBehaviour
{
    bool canButton = true;

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
                //blooner.balloonSpawner(); //Need to find a way to implement this
            }
        }
    }
}
