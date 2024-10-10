using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bballGameRestart : MonoBehaviour
{
    public GameObject gate;
    bool canButton = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canButton)
        {
            startBasketballGame();
        }
    }

    void startBasketballGame()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("ballButton"))
        {
            //Debug.Log("Ball button pressed!");
            OpenGate();
            StartCoroutine(BballTimer());
        }
    }

    IEnumerator BballTimer()
    {
        return null;
    }

    void OpenGate()
    {
        gate.transform.Rotate(-90,0,0,Space.Self);
    }

}
