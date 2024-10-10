using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bballGameRestart : MonoBehaviour
{
    public GameObject gate;
    public TextMeshPro timerText;
    public float inputSeconds;
    float timerSeconds;
    bool canButton = true;
    bool timerIsOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canButton)
        {
            timerSeconds = inputSeconds; // Reset timer when player goes to play the game again
            startBasketballGame();
        }

        if (timerIsOn)
        {
            if (timerSeconds <= 0)
            {
                timerIsOn = false;
                CloseGate();
            }
            else
            {
                timerSeconds -= Time.deltaTime;
                timerSignUpdate(timerSeconds);
            }
        }


    }

    void startBasketballGame()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("ballButton"))
        {
            Debug.Log(timerSeconds);
            OpenGate(); // Opens the gate to let the player grab basketballs
            timerIsOn = true;
        }
    }

    IEnumerator BballTimer()
    {
        if(timerSeconds > 0)
        {
            timerSeconds -= Time.deltaTime;
            timerSignUpdate(timerSeconds);
            //StartCoroutine(BballTimer());
        }
        yield break;
    }

    void timerSignUpdate(float textToDisplay)
    {
        textToDisplay += 1;
        float seconds = Mathf.FloorToInt(textToDisplay % 60); // Rounds down to nearest second
        timerText.text = seconds + " seconds";
    }

    void OpenGate()
    {
        gate.transform.Rotate(-90,0,0,Space.Self);
    }

    void CloseGate()
    {
        gate.transform.Rotate(90, 0, 0, Space.Self);
    }
}
