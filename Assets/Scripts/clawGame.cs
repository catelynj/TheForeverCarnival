using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class clawGame : MonoBehaviour
{
    public GameObject clawParent;
    public GameObject target;
    public GameObject clawReturn;
    public int timesMoved = 0;
    public bool isArmLowered = false;
    bool isStarted = false;

    private float downSpeed = 1.0f;
    private float speed = 1.0f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 movedirection;


    // Start is called before the first frame update
    void Start()
    {
        //clawParent = GameObject.FindGameObjectWithTag("clawParent");
        //Rigidbody clawBody = clawParent.GetComponent<Rigidbody>();
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
            clawParent.transform.position += speed * Time.deltaTime * movedirection;
        }

        if (Input.GetKeyDown(KeyCode.Space) & isStarted)
        {
            //Lower claw
            if (!isArmLowered)
            {
                isArmLowered = true;
                //Vector3.Lerp(clawParent.transform.position, clawParent.transform.position + target.transform.position, 1f / downSpeed * Time.deltaTime);

                StartCoroutine("LowerClaw");
            }
            else
            {
                isArmLowered = false;
                //Vector3.Lerp(clawParent.transform.position, clawParent.transform.position + clawReturn.transform.position, 1f / downSpeed * Time.deltaTime);

                StartCoroutine("RaiseClaw");
            }

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

    private IEnumerator LowerClaw()
    {
        
        Rigidbody clawBody = clawParent.GetComponent<Rigidbody>(); 

        clawBody.MovePosition(target.transform.position);
        
        Debug.Log("Claw lowered");
        yield return null;
    }

    private IEnumerator RaiseClaw()
    {
        Rigidbody clawBody = clawParent.GetComponent<Rigidbody>();

        clawBody.MovePosition(clawReturn.transform.position);

        Debug.Log("Claw raised");
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
