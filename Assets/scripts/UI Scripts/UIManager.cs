using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private GameObject messageCanvas = null;
    [SerializeField] private GameObject interactCanvas = null;

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
    private float raycastDistance = 3f;

    private void Start()
    {
        SetActiveHud(true);
        settingsOpen = false;
        inventoryOpen = false;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
        updateScoreCall = false;
        pointSource = GetComponent<AudioSource>();
        interactCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentPrizeModel == null)
        {
            if (settingsOpen) //if settings already open, close
            {
                SetActiveSettings(false);
                SetActiveHud(true);
            }
            else
            {
                SetActiveSettings(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && currentPrizeModel == null)
        {
            if (inventoryOpen) //if inventory already open, close
            {
                SetActiveInventory(false);
                SetActiveHud(true);
            }
            else
            {
                SetActiveInventory(true);
            }
        }

        // Check for input to destroy the current prize model
        if (currentPrizeModel != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            InputSystem.EnableDevice(Keyboard.current);
            ClearScreen(false); // Reactivate canvases
            Destroy(currentPrizeModel);
            currentPrizeModel = null;
            HideMessage();
        }

        //Press E to Interact Message
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, raycastDistance))
        {
            if(hit.collider.CompareTag("Interact"))
            {
                interactCanvas.SetActive(true);
            }
        }
    }

    public void SetActiveHud(bool isPlaying)
    {
        hudCanvas.SetActive(isPlaying);
        settingsCanvas.SetActive(!isPlaying);
        inventoryCanvas.SetActive(!isPlaying);

        FirstPersonController.Instance.enabled = isPlaying;
    }

    public void SetActiveSettings(bool isPaused)
    {
        settingsCanvas.SetActive(isPaused);
        hudCanvas.SetActive(!isPaused);
        inventoryCanvas.SetActive(!isPaused);
        settingsOpen = isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FirstPersonController.Instance.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            FirstPersonController.Instance.enabled = true;
        }
    }

    public void SetActiveInventory(bool isInventory)
    {
        inventoryCanvas.SetActive(isInventory);
        hudCanvas.SetActive(!isInventory);
        settingsCanvas.SetActive(!isInventory);
        inventoryOpen = isInventory;

        if (isInventory)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FirstPersonController.Instance.enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FirstPersonController.Instance.enabled = true;
        }
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
        if (index < prizePrefabs.Length)
        {
            GameObject prizePrefab = prizePrefabs[index];
            if (GameManager.Instance.Inventory.Count > 0)
            {
                Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 1.3f));
                spawnPosition.y -= 0.3f;
                currentPrizeModel = Instantiate(prizePrefab, spawnPosition, Quaternion.Euler(0,0,0));
                

                TrophyController trophyController = currentPrizeModel.GetComponent<TrophyController>();
                Animator trophyAnim = currentPrizeModel.GetComponent<Animator>();

                if (trophyController != null && trophyAnim != null)
                {
                    trophyAnim.SetBool("IsInventory", true);
                    trophyController.StartRotation();
                    InputSystem.DisableDevice(Keyboard.current);
                    DisplayMessage("Click to put trophy away");
                    ClearScreen(true);
                }
                else
                {
                    Debug.LogError("TrophyController null");
                }
            }
        }
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

    public void Quit()
    {
        Application.Quit();
    }
}
