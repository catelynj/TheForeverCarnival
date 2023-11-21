using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPopper : MonoBehaviour
{
    // Add any balloon-related variables here.

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
        // Add balloon-popping logic here.
        // For example, you can play a popping sound, instantiate a particle effect, and then destroy the balloon object.
        Debug.Log("Balloon popped!");

        // Add your balloon-popping effects and cleanup code here.
        // For example, you might play a popping sound, instantiate a particle effect, and then destroy the balloon object.
        // For simplicity, let's just destroy the balloon in this example.
        Destroy(gameObject);
    }
}