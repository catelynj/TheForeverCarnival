using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupPoints : MonoBehaviour
{
    private int cupScore = 100;
    public AudioClip cupSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cup"))
        {
            //GameManager.Instance.cupSource.Play();
            GameManager.Instance.IncrementScore(cupScore);
            if (audioSource != null && cupSound != null)
            {
               audioSource.PlayOneShot(cupSound);
            }
               
           
            Destroy(other.gameObject, 0.5f);
        }
    }

}
