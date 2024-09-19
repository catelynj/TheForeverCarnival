using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBoothCollision : MonoBehaviour
{
    private BoxCollider bottomCol;
    // Start is called before the first frame update
    void Start()
    {
        bottomCol = GetComponent<BoxCollider>();
   }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Cup"))
        {
            Debug.Log("cup point");
        }
    }
}
