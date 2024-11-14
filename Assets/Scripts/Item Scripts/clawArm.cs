using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class clawArm : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trophy"))
        {
            CloseArm();
        }
    }

    private void CloseArm()
    {
        //This method will close the arm
        Debug.Log("ClawArm closing");
    }

}
