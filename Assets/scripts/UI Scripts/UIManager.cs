using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;

      
    }

    private bool settingsOpen = false;
    private bool inventoryOpen = false;
    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject settingsCanvas = null;
    [SerializeField] private GameObject inventoryCanvas = null;
    public Text messageText;
    public bool updateScoreCall = false;
    public Text scoreText;

    private void Start()
    {
        SetActiveHud(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        updateScoreCall = false;
    }

    private void Update()
    {

        //press Escape to bring up settings, press again to close settings
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsOpen)
        {
            SetActiveSettings(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && settingsOpen)
        {
            SetActiveSettings(false);
            SetActiveHud(true);
        }

        //press Tab to bring up inventory, press again to close inventory
        if (Input.GetKeyDown(KeyCode.Tab) && !inventoryOpen)
        {
            SetActiveInventory(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen)
        {
            SetActiveInventory(false);
            SetActiveHud(true);
        }
    }

    public void SetActiveHud(bool isPlaying)
    {
        hudCanvas.SetActive(isPlaying);
        settingsCanvas.SetActive(!isPlaying);
        inventoryCanvas.SetActive(!isPlaying);
    }

    public void SetActiveSettings(bool isPaused)
    {
        settingsCanvas.SetActive(isPaused);
        hudCanvas.SetActive(!isPaused);
        inventoryCanvas.SetActive(!isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState= CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        settingsOpen = isPaused;
    }

    public void SetActiveInventory(bool isInventory)
    {
        inventoryCanvas.SetActive(isInventory);
        hudCanvas.SetActive(!isInventory);
        settingsCanvas.SetActive(!isInventory);
        if (isInventory)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        inventoryOpen = isInventory;
    }

    public void UpdateScore()
    {
        //adjust logic for multiple games affecting score
        if (updateScoreCall)
        {
            scoreText.text = GameManager.Instance.globalScore.ToString();
        }
        updateScoreCall = false;
       
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
            messageText.gameObject.SetActive(true);
        }
    }
    public void HideMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
            messageText.gameObject.SetActive(false);
        }
    }
}
