using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopper : MonoBehaviour
{
    // Add any balloon-related variables here.
    public GameObject balloon;
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a "Dart" tag.
        if (collision.gameObject.CompareTag("Dart"))
        {
            // Call a method to handle balloon popping.
            PopBalloon();
        }
    }

    void PopBalloon()
    {
        //Debug.Log("Balloon popped!");
        Destroy(balloon);
    }
}