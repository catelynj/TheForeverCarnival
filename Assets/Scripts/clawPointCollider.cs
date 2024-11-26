using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clawPointCollider : MonoBehaviour
{
    public int prizePoints = 700;

    void Start()
    {
        GameObject picture = GameObject.Find("picture");
        Transform pictureTransform = picture.transform;
        GameObject CubeTrophy = GameObject.Find("CubeTrophy");
        Transform cubeTransform = CubeTrophy.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trophy"))
        {
            GameManager.Instance.IncrementScore(prizePoints);
            Destroy(other.gameObject);
            //if (other.gameObject.Equals("picture"))
            //{
            //    other.gameObject.transform.position = pictureTransform.position;
            //}
        }
    }
}
