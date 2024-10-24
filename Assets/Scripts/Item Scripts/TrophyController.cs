using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyController : MonoBehaviour
{
    private Animator anim;
    public int prizePrice;
 
   // private GameObject prize;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsInventory", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.globalScore >= prizePrice )
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Trophy"))
            {
                Debug.Log("hit" + hit.collider.gameObject);
                GameManager.Instance.globalScore -= prizePrice;
                GameManager.Instance.AddToInventory(hit.collider.gameObject);

                hit.collider.gameObject.SetActive(false);

            }
        }
    }

    public void StartSpin()
    {
        if (anim != null)
        {
            anim.SetBool("IsInventory", true); // Start the spinning animation
            Debug.Log("Spin started on: " + gameObject.name);
        }
        else if(anim == null)
        {
            Debug.Log("didnt work");
        }
    }
}
