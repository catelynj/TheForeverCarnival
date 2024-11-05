using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clawGame : MonoBehaviour
{
    GameObject claw;
    // Start is called before the first frame update
    void Start()
    {
        claw = GameObject.FindGameObjectWithTag("clawArm");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            canClawGame();
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
         * - Disable player movement
         * - Route W A S D to the claw's movement
         * - Have the claw lower when space bar is pressed, then raise after a given amount (like 1f)
         * - on Input.GetKeyDown(KeyCode.E), claws release and the player exits the game
         * - Restore player movement
         */

        //This technically disables player movement
        FirstPersonController.MoveSpeed = 0f;
        FirstPersonController.SprintSpeed = 0f;
        FirstPersonController.JumpHeight = 0f;

        Destroy(claw, 0f);
        Debug.Log("Claw game began");

        //This technically reenables player movement
        FirstPersonController.MoveSpeed = 4f;
        FirstPersonController.SprintSpeed = 6f;
        FirstPersonController.JumpHeight = 1.6f;

    }

}
