using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
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
    [SerializeField] private Score score;


    private void Start()
    {
        SetActiveHud(true);
        Cursor.visible = false;
        LockCursor();
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
           UnlockCursor();
        }
        else
        {
            LockCursor();
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
            UnlockCursor();
            Cursor.visible = true;
        }
        else
        {
            LockCursor();
            Cursor.visible = false;
        }


        inventoryOpen = isInventory;
    }

    public void UpdateScore()
    {
        score.AddScore();
    }
    public void Quit()
    {
        Application.Quit();
    }

   
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.lockState -= CursorLockMode.None;
    }
}
