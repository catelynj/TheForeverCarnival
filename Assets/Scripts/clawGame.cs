using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class clawGame : MonoBehaviour
{
    public Transform clawParent;
    public bool isArmLowered = false;

    private float downSpeed = 0.8f;
    private float speed = 0.8f;
    private float downDistance = 1f;
    private float initialClawY;
    private float initialClawX;
    private float initialClawZ;
    private bool isStarted = false;
    private Vector3 movedirection;

    // This is a bad way to stop the claw from moving out of bounds
    private const float minX = 52.3f;
    private const float maxX = 54.75f;
    private const float minZ = -8.5f;
    private const float maxZ = -7f;

    // Start is called before the first frame update
    void Start()
    {
        initialClawY = clawParent.position.y;
        initialClawX = clawParent.position.x;
        initialClawZ = clawParent.position.z;
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

            // Clamp position to bounds
            Vector3 clampedPosition = clawParent.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
            clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
            clawParent.position = clampedPosition;
            // This is a terrible way to do this^
        }

        if (Input.GetKeyDown(KeyCode.Space) & isStarted)
        {
            //Lower claw
            if (!isArmLowered)
            {
                StartCoroutine("DropClaw");
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

    void exitClawGame()
    {
        //This technically reenables player movement
        FirstPersonController.MoveSpeed = 4f;
        FirstPersonController.SprintSpeed = 6f;
        FirstPersonController.JumpHeight = 1.6f;
        isStarted = false;
    }

}
