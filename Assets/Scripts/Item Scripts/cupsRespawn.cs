using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupsRespawn : MonoBehaviour
{
    public GameObject cups;
    bool canButton = true;
    public GameObject spawnArea;
    IEnumerator cupRespawner;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CupsSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canButton)
        {
            respawnCups();
        }
    }

    void respawnCups()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("cupButton"))
        {
            Debug.Log("Button pressed!");
            StartCoroutine(CupsSpawner());
        }
    }

    IEnumerator CupsSpawner()
    {
        canButton = false;
        Collider spawn = spawnArea.GetComponent<BoxCollider>();
        Rigidbody rigi = cups.GetComponent<Rigidbody>();

        Vector3 spawnLocation = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, spawnArea.transform.position.z);
        Instantiate(cups, spawnLocation, Quaternion.identity);

        Debug.Log("Waiting..." + waitTime);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Ready!" + waitTime);
        canButton = true;
    }

}
