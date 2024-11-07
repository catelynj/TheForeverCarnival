using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class clawGame : MonoBehaviour
{
    GameObject clawParent;
    bool isStarted = false;

    private float speed = 10.0f;
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
         * - Route W A S D to the claw's movement
         * - Have the claw lower when space bar is pressed, then raise after a given amount (like 1f)
         * - on Input.GetKeyDown(KeyCode.E), claws release (not done) and the player exits the game (done)
         * - Restore player movement
         */

        isStarted = true;

        //This technically disables player movement
        FirstPersonController.MoveSpeed = 0f;
        FirstPersonController.SprintSpeed = 0f;
        FirstPersonController.JumpHeight = 0f;

        // Route WASD to object with clawParent tag



        
        Debug.Log("Claw game began");
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
