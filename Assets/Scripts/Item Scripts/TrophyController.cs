using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;



public class TrophyController : MonoBehaviour
{
    public int prizePrice;
    public UIManager.Prize prize;
    string prizeName;
    
    private Camera mainCamera;
    private Vector3 previousMousePosition;

    private void Start()
    {
        if (prize != null)
        {
            prizeName = prize.name;
            Debug.Log("Prize name: " + prizeName);
        }
        else
        {
            Debug.LogError("Prize is not assigned!");
        }
        mainCamera = Camera.main;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.globalScore >= prizePrice)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            //add prize to player inventory
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Trophy"))
            {
                TrophyController trophy = hit.collider.GetComponent<TrophyController>();
                if (trophy != null && GameManager.Instance.CanAddToInventory(trophy.prizeName))
                {
                    GameManager.Instance.globalScore -= prizePrice;
                    GameManager.Instance.AddToInventory(trophy.prizeName);
                    //disable prize on wall
                    hit.collider.gameObject.SetActive(false);
                    UIManager.Instance.UpdateScore();
                }
                else
                {
                    Debug.LogWarning("Failed to add trophy: either inventory is full or prize name is invalid.");
                }
            }
        }
    }
    void OnMouseDown()
    {
        previousMousePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        //inspect function
        Vector3 delta = Input.mousePosition - previousMousePosition;

        transform.Rotate(Vector3.up, delta.x * 0.25f, Space.Self);
        transform.Rotate(Vector3.right, delta.y * 0.25f, Space.Self);

        previousMousePosition = Input.mousePosition;
    }
}
