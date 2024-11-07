using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BulletCollider : MonoBehaviour
{
    // score increase on hit
    int gunScore = 100;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            // increment score if hits target
            GameManager.Instance.IncrementScore(gunScore);
        }
    }
}
