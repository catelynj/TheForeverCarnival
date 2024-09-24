using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonPopper : MonoBehaviour
{
    public GameObject balloon;
    public AudioClip popSound;  
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

      /*  if (audioSource != null && popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }
      */
        audioSource.PlayOneShot(popSound);
        UIManager.instance.UpdateScore();
        Destroy(balloon);
    }
}