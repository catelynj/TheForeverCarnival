using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockArea : MonoBehaviour
{

    private float messageTimer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.E) && Physics.Raycast(ray, out hit) && hit.collider.CompareTag("unlockButton"))
        {
            if(GameManager.Instance.globalScore == 3000)
            {
                Debug.Log("Unlock Next Area");
                
            }
            else
            {
                UIManager.instance.DisplayMessage("You need 3000 points to unlock this area.");
                StartCoroutine(HideMessageAfterDelay(messageTimer));
            }
           
        }
        
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.instance.HideMessage(); // Make sure you have this method in UIManager
    }
}
