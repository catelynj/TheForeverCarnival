using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupPoints : MonoBehaviour
{
    public AudioSource cupSound;
    private int cupScore = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cup"))
        {
            cupSound.Play();
            GameManager.Instance.IncrementScore(cupScore);
        }
    }

}
