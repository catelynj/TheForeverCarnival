using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class clawGame : MonoBehaviour
{
    GameObject clawParent;
    bool isStarted = false;

    private float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movedirection;


    // Start is called before the first frame update
    void Start()
    {
        clawParent = GameObject.FindGameObjectWithTag("clawParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isStarted)
        {
            canClawGame();
        }
        else if(Input.GetKeyDown(KeyCode.E) && isStarted)
        {
            exitClawGame();
        }

        if (isStarted)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            movedirection = new Vector3(horizontalInput, 0, verticalInput);
            clawParent.transform.position += movedirection * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) & isStarted)
        {
            //Lower claw
            StartCoroutine("LowerClaw");
        }

    }

    void canClawGame()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("clawButton"))
        {
            startClawGame();
        }
    }

    void startClawGame()
    {
        /*
         * We want to:
         * - Disable player movement - Done
         * - Route W A S D to the claw's movement - Done
         * - Have the claw lower when space bar is pressed, then raise after a given amount (like 1f)
         * - on Input.GetKeyDown(KeyCode.E), claws release (not done) and the player exits the game (done)
         * - Restore player movement - Done
         */

        isStarted = true;

        //This technically disables player movement
        FirstPersonController.MoveSpeed = 0f;
        FirstPersonController.SprintSpeed = 0f;
        FirstPersonController.JumpHeight = 0f;

        
        Debug.Log("Claw game began");
    }

    private IEnumerator LowerClaw()
    {
        GameObject clawParent = GameObject.FindGameObjectWithTag("clawParent");
        GameObject target = GameObject.FindGameObjectWithTag("test");
        float speed = 0.5f;
        float startTime = Time.time;
        Transform startPos = clawParent.transform;
        Transform endPos =  target.transform;

        float moveTime = Vector3.Distance(startPos.position, endPos.position);
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / moveTime;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);

        yield return null;
    }

    void exitClawGame()
    {
        //This technically reenables player movement
        FirstPersonController.MoveSpeed = 4f;
        FirstPersonController.SprintSpeed = 6f;
        FirstPersonController.JumpHeight = 1.6f;
        isStarted = false;
    }

}
