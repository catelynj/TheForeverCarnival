using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private bool isInsideCollider = false;
    private float interactionTimer = 0f;
    private float interactionDuration = 8f; // Adjust the duration as needed
    private string[] messages = { "You cannot leave.", "Do you think this is a game?", "Turn around." };
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
            UIManager.instance.HideMessage();
            interactionTimer = 0f;
            currentMessageIndex = 0; // Reset to the first message when exiting
        }
    }

    private void Update()
    {
        if (isInsideCollider)
        {
            interactionTimer += Time.deltaTime;

            if (interactionTimer >= interactionDuration)
            {
                // Display the next message after the specified duration
                DisplayNextMessage();
            }
        }
    }

    private void DisplayNextMessage()
    {
        if (currentMessageIndex < messages.Length)
        {
            UIManager.instance.DisplayMessage(messages[currentMessageIndex]);
            currentMessageIndex++;
            interactionTimer = 0f; // Reset the timer for the next message
        }
    }
}
