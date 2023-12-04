using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BalloonPopper : MonoBehaviour

{
    
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
        UIManager.instance.UpdateScore();
        Destroy(balloon);
    }
}