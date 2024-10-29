using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    public int prizePrice;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Add trophy to inventory
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.globalScore >= prizePrice)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Trophy"))
            {
                Debug.Log("hit " + hit.collider.gameObject);
                GameManager.Instance.globalScore -= prizePrice;
                GameManager.Instance.AddToInventory(hit.collider.gameObject);
                hit.collider.gameObject.SetActive(false);
            }
        }

    }

    public void StartRotation()
    {
        Debug.Log("Rotation Start");

    }

}
