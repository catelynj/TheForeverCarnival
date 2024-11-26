using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class clawGame : MonoBehaviour
{
    public Transform clawParent;
    public bool isArmLowered = false;

    private float downSpeed = 0.8f;
    private float speed = 0.8f;
    private float downDistance = 1f;
    private float initialClawY;
    private bool isStarted = false;
    private Vector3 movedirection;


    // Start is called before the first frame update
    void Start()
    {
        initialClawY = clawParent.position.y;
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
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 move = new Vector3(horizontalInput, 0, verticalInput) * speed * Time.deltaTime;
            clawParent.Translate(move, Space.World);
            //movedirection = new Vector3(horizontalInput, 0, verticalInput);
            //clawParent.transform.position += speed * Time.deltaTime * movedirection;
        }

        if (Input.GetKeyDown(KeyCode.Space) & isStarted)
        {
            //Lower claw
            if (!isArmLowered)
            {
                //Vector3.Lerp(clawParent.transform.position, clawParent.transform.position + target.transform.position, 1f / downSpeed * Time.deltaTime);

                StartCoroutine("DropClaw");
            }
            //else
            //{
            //    isArmLowered = false;
            //    //Vector3.Lerp(clawParent.transform.position, clawParent.transform.position + clawReturn.transform.position, 1f / downSpeed * Time.deltaTime);

            //    StartCoroutine("RaiseClaw");
            //}

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

    }

    System.Collections.IEnumerator DropClaw()
    {
        isArmLowered = true;

        float targetY = initialClawY - downDistance;
        while (clawParent.position.y > targetY)
        {
            clawParent.Translate(Vector3.down * downSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        
        while(clawParent.position.y < initialClawY)
        {
            clawParent.Translate(Vector3.up * downSpeed * Time.deltaTime);
            yield return null;
        }
        isArmLowered = false;
    }

    //private IEnumerator LowerClaw()
    //{
        
    //    Rigidbody clawBody = clawParent.GetComponent<Rigidbody>(); 
        
    //    Debug.Log("Claw lowered");
    //    yield return null;
    //}

    //private IEnumerator RaiseClaw()
    //{
    //    Rigidbody clawBody = clawParent.GetComponent<Rigidbody>();

    //    Debug.Log("Claw raised");
    //    yield return null;
    //}

    void exitClawGame()
    {
        //This technically reenables player movement
        FirstPersonController.MoveSpeed = 4f;
        FirstPersonController.SprintSpeed = 6f;
        FirstPersonController.JumpHeight = 1.6f;
        isStarted = false;
    }

}
