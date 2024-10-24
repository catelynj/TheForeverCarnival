using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    public int globalScore;
    public Vector3 playerLocation;

    private AudioSource backgroundSource;
    public AudioClip backgroundSound;

    public List<GameObject> Inventory = new List<GameObject>();
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
        //globalScore = 0;
        //pointSource = GetComponent<AudioSource>();

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

    public void AddToInventory(GameObject item)
    {
        if (!Inventory.Contains(item))
        {
            Inventory.Add(item);
            UIManager.Instance.UpdateInventoryCanvas(Inventory.Count - 1); 
        }
        else
        {
            Debug.Log("Item already exists");
        }
    }


    /***********************************/
    /* Player save/load functionality  */ //In progress
    /***********************************/
    /**/
    public void Load()
    {
        string filePath = Application.persistentDataPath + "/player.save";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveState playerData = JsonUtility.FromJson<SaveState>(json);

            globalScore = playerData.score;
            playerLocation = playerData.playerPosition;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = playerLocation;
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
