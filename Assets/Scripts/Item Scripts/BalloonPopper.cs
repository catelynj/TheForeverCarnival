using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonPopper : MonoBehaviour
{
    public GameObject balloon;
    public int balloonScore = 100;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dart"))
        {
            PopBalloon();
        }
    }

    void PopBalloon()
    {
        GameManager.Instance.IncrementScore(balloonScore);
        Destroy(balloon);
    }
}