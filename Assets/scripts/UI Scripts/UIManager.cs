using JetBrains.Annotations;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    //singleton pattern
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
    [SerializeField] public GameObject inventoryCanvas = null;
    [SerializeField] private GameObject messageCanvas = null;
    public GameObject interactCanvas = null;
    private float raycastDistance = 3.8f; //how far interact message pops up

    public Text messageText;
    public bool updateScoreCall = false;
    public Text scoreText;
    public Text prizeCount;

    public AudioClip pointSound;
    private AudioSource pointSource;

    public Image[] inventoryImages;
    public Sprite placeholder;
    public int currentInventoryCount = 0;
    private GameObject currentPrizeModel;

    [System.Serializable]
    public class Prize
    {
        public string name;
        public Sprite sprite;
        public GameObject prefab;
    }

    public List<Prize> prizes = new List<Prize>();
    public Dictionary<string, Prize> prizeDictionary = new Dictionary<string, Prize>();
   
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

        //prize object setup
        foreach (var prize in prizes)
        {
            if (!prizeDictionary.ContainsKey(prize.name))
            {
                prizeDictionary[prize.name] = prize;
                //Debug.Log($"Prize added: {prize.name}");
            }
        }

  

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

        //Check for input to close interact screen & return to inventory
        if (currentPrizeModel != null && Input.GetKeyDown(KeyCode.Escape))
        {
            ClearScreen(false);
            Destroy(currentPrizeModel);
            currentPrizeModel = null;
            HideMessage();
        }

        //Press E to Interact Message
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if(hit.collider.CompareTag("Interact") || hit.collider.CompareTag("Trophy") || hit.collider.CompareTag("dartButton") 
                || hit.collider.CompareTag("cupButton") || hit.collider.CompareTag("ballButton") || hit.collider.CompareTag("clawButton") || hit.collider.CompareTag("Gun") 
                || hit.collider.CompareTag("Dart") || hit.collider.CompareTag("Ball") || hit.collider.CompareTag("basketball"))
            {
                interactCanvas.SetActive(true);
            }
            else
            {
                interactCanvas.SetActive(false);
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
            //disable mouse and keyboard movement
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
            //disable mouse and keyboard movement
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
        //set all huds to inactive for inspect function
        if (clear)
        {
            hudCanvas.SetActive(false);
            settingsCanvas.SetActive(false);
            inventoryCanvas.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
    public void UpdateInventoryCanvas(string prizeName)
    {
        //if prize name is in prize dictionary
        if (prizeDictionary.TryGetValue(prizeName, out Prize prize))
        {
            int prizeIndex = prizes.IndexOf(prizes.Find(p => p.name == prizeName));

            if (prizeIndex >= 0 && prizeIndex < inventoryImages.Length)
            {
                inventoryImages[prizeIndex].sprite = prize.sprite;
                currentInventoryCount++;

                //update UI to show prize count in the bottom left
                prizeCount.text = currentInventoryCount.ToString();
            }
        }
        else
        {
            Debug.LogError($"Prize with name {prizeName} not found.");
        }
    }

    public void OnInventoryClick(string prizeName)
    {
        //check if prize is in dictionary and if player has already bought it
        if (prizeDictionary.TryGetValue(prizeName, out Prize prize) && GameManager.Instance.inventory.Contains(prizeName))
        {
            if (GameManager.Instance.inventory.Count > 0 && currentPrizeModel == null)
            {
                Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 2f));
                //spawn prize in front of player at eye-ish level -- some are too high, some are too far away 
                if (prizeName == "RobotPrize" || prizeName == "PenguinPrize" || prizeName =="FerrisPrize" || prizeName == "CatPrize")
                {
                    spawnPosition.y = 1f;
                }
                else
                    spawnPosition.y = 1.4f;

                if (prizeName == "CatPrize" || prizeName == "GummyPrize")
                {
                    spawnPosition.z += 1f;
                } 
                
                currentPrizeModel = Instantiate(prize.prefab, spawnPosition, Quaternion.identity);
                TrophyController trophyController = currentPrizeModel.GetComponent<TrophyController>();

                if (trophyController != null)
                {
                    ClearScreen(true);
                    DisplayMessage("Click and Drag to Inspect Prize \n Press Escape to Leave Inspect");
                }
                else
                {
                    Debug.LogError("TrophyController null");
                }
            }
        }
        else
        {
            //Debug.LogError($"Prize with name {prizeName} not found.");
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
