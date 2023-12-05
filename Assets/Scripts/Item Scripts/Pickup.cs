using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayer;

    private Camera cam;
    private Inventory inventory;

    private void Start()
    {
        cam = Camera.main;
        inventory = GetComponent<Inventory>();

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                Item2 newItem = hit.transform.GetComponent<ItemObject>().item as Item2;
                inventory.AddItem(newItem);
                Destroy(hit.transform.gameObject);
            }

        }
    }


}
