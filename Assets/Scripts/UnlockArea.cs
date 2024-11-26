using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockArea : MonoBehaviour
{

    private float messageTimer = 3f;
    public int unlockAmount = 0;
    public GameObject unlockBarrier;
    public GameObject gameBooth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Input.GetKeyUp(KeyCode.E) && Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Interact"))
        {
            if (GameManager.Instance.globalScore >= unlockAmount)
            {
                GameManager.Instance.globalScore -= unlockAmount;
                UIManager.Instance.updateScoreCall = true;
                UIManager.Instance.UpdateScore();
                Destroy(unlockBarrier);
                gameBooth.SetActive(true);
            }
            else
            {
                UIManager.Instance.DisplayMessage("Insufficient Funds...Bozo");
                StartCoroutine(HideMessageAfterDelay(messageTimer));
            }
        }
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIManager.Instance.HideMessage();
    }

}
