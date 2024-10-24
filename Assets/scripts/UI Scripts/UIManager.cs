using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private bool settingsOpen = false;
    private bool inventoryOpen = false;
    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject settingsCanvas = null;
    [SerializeField] private GameObject inventoryCanvas = null;
    public Text messageText;
    public bool updateScoreCall = false;
    public Text scoreText;
    public AudioClip pointSound;
    private AudioSource pointSource;
    public Image[] inventoryImages;
    public Sprite[] prizeSprites;
    private int inventorySlot = 0;
    public int currentInventoryCount = 0;
    public GameObject[] prizePrefabs;
    private GameObject currentPrizeModel;
    private void Start()
    {
        SetActiveHud(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        updateScoreCall = false;
        pointSource = GetComponent<AudioSource>();
       
    }

    private void Update()
    {

        // press Escape to bring up settings, press again to close settings
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsOpen)
        {
            SetActiveSettings(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && settingsOpen)
        {
            SetActiveSettings(false);
            SetActiveHud(true);
        }

        // press Tab to bring up inventory, press again to close inventory
        if (Input.GetKeyDown(KeyCode.Tab) && !inventoryOpen)
        {
            SetActiveInventory(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && inventoryOpen)
        {
            SetActiveInventory(false);
            SetActiveHud(true);
        }

        // check for input to destroy the current prize model
        if (currentPrizeModel != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
           InputSystem.EnableDevice(Keyboard.current);
           ClearScreen(false); //reactivate canvases
           Destroy(currentPrizeModel);
           currentPrizeModel = null;
            
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

    public void ClearScreen(bool clear)
    {
        if (clear)
        {
            hudCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            inventoryCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            SetActiveInventory(true);
        }
    }

    public void UpdateScore()
    {
        
        if (updateScoreCall)
        {
            scoreText.text = GameManager.Instance.globalScore.ToString();
            //Debug.Log("Point");
            pointSource.PlayOneShot(pointSound);
        }
        GameManager.Instance.globalScore = int.Parse(scoreText.text);
        updateScoreCall = false;
    }

    public void UpdateInventoryCanvas(int prizeIndex)
    {
        if (inventorySlot < inventoryImages.Length && prizeIndex < prizeSprites.Length)
        {
            inventoryImages[inventorySlot].sprite = prizeSprites[prizeIndex];
            inventoryImages[inventorySlot].enabled = true;
            inventorySlot++;
        }
    }

    public void OnInventoryClick(int index)
    {
        
        if (index < prizePrefabs.Length )
        {
            GameObject prizePrefab = prizePrefabs[index];


            Debug.Log("Current Inventory: " + string.Join(", ", GameManager.Instance.Inventory.Select(item => item.name)));

            if (GameManager.Instance.Inventory.Count > 0)
            {
                Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1)); // Adjust Z distance for visibility
                spawnPosition.y -= 0.5f;
                currentPrizeModel = Instantiate(prizePrefabs[index], spawnPosition, Quaternion.identity);
                ClearScreen(true);
                InputSystem.DisableDevice(Keyboard.current);
            }
           
        }
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
