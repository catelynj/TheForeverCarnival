using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private bool isInsideCollider = false;
    private float interactionTimer = 0f;
    private float interactionDuration = 4f;
    private string[] messages = { "You cannot leave.", "Do you think this is a game?", "Seriously...", "Turn around." };
    private int currentMessageIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCollider = true;
            DisplayNextMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideCollider = false;
            UIManager.Instance.HideMessage();
            interactionTimer = 0f;
            currentMessageIndex = 0;
        }
    }

    private void Update()
    {
        if (isInsideCollider)
        {
            interactionTimer += Time.deltaTime;

            if (interactionTimer >= interactionDuration)
            {
                DisplayNextMessage();
            }
        }
    }

    private void DisplayNextMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            UIManager.Instance.DisplayMessage(messages[currentMessageIndex]);
            currentMessageIndex++;
            interactionTimer = 0f;
        }
    }
}
