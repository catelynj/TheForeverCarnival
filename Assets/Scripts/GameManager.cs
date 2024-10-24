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

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = new GameObject("GameManager").AddComponent<GameManager>(); //create game manager object if required
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
        UIManager.instance.updateScoreCall = true;
        UIManager.instance.UpdateScore();

        //Save();

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
            UIManager.instance.scoreText.text = playerData.score.ToString();
        }

    }

    public void Save()
    {
        globalScore = int.Parse(UIManager.instance.scoreText.text);
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
