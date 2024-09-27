using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonPopper : MonoBehaviour
{
    public GameObject balloon;
    public AudioClip popSound;
    private AudioSource popSource;
    public int balloonScore = 100;

    private void Start()
    {
        popSource = GetComponent<AudioSource>();
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
        if (popSource != null && popSound != null)
        {
            popSource.PlayOneShot(popSound);
        }
        GameManager.Instance.IncrementScore(balloonScore);
        Destroy(balloon, popSound.length - 0.78f);
    }
}