using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    public int prizePrice;

    private Animator anim;

    public UIManager.Prize prize;
    string prizeName;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (prize != null)
        {
            prizeName = prize.name;
            Debug.Log("Prize name: " + prizeName);
        }
        else
        {
            Debug.LogError("Prize is not assigned!");
        }
        

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
                TrophyController trophy = hit.collider.GetComponent<TrophyController>();
                if (trophy != null && GameManager.Instance.CanAddToInventory(trophy.prizeName))
                {
                    GameManager.Instance.globalScore -= prizePrice;
                    GameManager.Instance.AddToInventory(trophy.prizeName);
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

    public void StartRotation()
    {
        Debug.Log("Rotation Start");
        if (anim != null)
        {
            anim.SetBool("IsRotating", true); // Trigger rotation animation
        }
    }
}
