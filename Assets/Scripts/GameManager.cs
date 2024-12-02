using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UIManager;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    public int globalScore;
    public Vector3 playerLocation;
    private GameObject player;
    private AudioSource backgroundSource;
    public AudioClip backgroundSound;
    public int inventoryCapacity = 10;
    public List<string> inventory = new List<string>();

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    instance = gameManagerObject.AddComponent<GameManager>();
                    DontDestroyOnLoad(gameManagerObject);
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        InputSystem.EnableDevice(Keyboard.current);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        backgroundSource = GetComponent<AudioSource>();

        if (backgroundSource != null && backgroundSound != null)
        {
            backgroundSource.Play();
        }
    }

    public void IncrementScore(int score)
    {

        globalScore += score;
        UIManager.Instance.updateScoreCall = true;
        UIManager.Instance.UpdateScore();

        //Save();

    }
    public bool CanAddToInventory(string prizeName)
    {
        //check if inventory is full and if prize is already bought
        if (inventoryCapacity <= 0) return false;
        return inventory.Count < inventoryCapacity && !inventory.Contains(prizeName);
    }


    public void AddToInventory(string prizeName)
    {
        if (UIManager.Instance.prizeDictionary.TryGetValue(prizeName, out Prize prize))
        {
            inventory.Add(prizeName);
            UIManager.Instance.UpdateInventoryCanvas(prizeName);
        }
        else
        {
            Debug.LogError($"Prize with name {prizeName} not found.");
        }
    }

    /***********************************/
    /* Player save/load functionality  */ //In progress
    /***********************************/
    /**/
    public void Load()
    {
        string filePath = Application.persistentDataPath + "/player.save";

        player.transform.SetPositionAndRotation(player.transform.position, player.transform.rotation);
        Physics.SyncTransforms();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveState playerData = JsonUtility.FromJson<SaveState>(json);

            globalScore = playerData.score;
            playerLocation = playerData.playerPosition;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.transform.position = playerLocation;
            player.GetComponent<FirstPersonController>().enabled = false;
            player.transform.position = new Vector3(playerLocation.x, playerLocation.y, playerLocation.z);
            player.GetComponent<FirstPersonController>().enabled = true;
            UIManager.Instance.scoreText.text = playerData.score.ToString();
        }

    }

    public void Save()
    {
        globalScore = int.Parse(UIManager.Instance.scoreText.text);
        var playerData = new SaveState { score = globalScore, playerPosition = PlayerPos() };

        //Uses JSON to save and load data
        string json = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/player.save", json);
    }

    private Vector3 PlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

}
