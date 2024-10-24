using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bballPointCollider : MonoBehaviour
{
    public int ballPoints = 150;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("basketball"))
        {
            GameManager.Instance.IncrementScore(ballPoints);
        }
    }

}
