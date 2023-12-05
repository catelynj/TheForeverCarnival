using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickupRange;
    public LayerMask pickupLayer;

    public Camera cam;
    private Inventory inventory;

    private void Start()
    {
        cam = Camera.main;
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
     
    }

 
}
