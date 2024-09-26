using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupPoints : MonoBehaviour
{
    private int cupScore = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cup"))
        {
            GameManager.Instance.cupSource.Play();
            GameManager.Instance.IncrementScore(cupScore);
        }
    }

}
