using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bballPointCollider : MonoBehaviour
{
    private int ballPoints = 150;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("basketball"))
        {
            //GameManager.Instance.cupSource.Play();
            GameManager.Instance.IncrementScore(ballPoints);
        }
    }

}
