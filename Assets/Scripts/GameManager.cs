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
        globalScore = 0;
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
    }

    
      /***********************************/
     /* Player save/load functionality  */ //In progress
    /***********************************/
    /**/
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState playerData = (SaveState)bf.Deserialize(fileStream);
            fileStream.Close();

            globalScore = playerData.score;
            playerLocation = playerData.getPlayerLocation();

        }

    }

    public void Save()
    {
        var saveClass = new SaveState();
        SaveState playerData = new SaveState { score = globalScore, playerPosition = PlayerPos() };

        Debug.Log(playerData);

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(Application.persistentDataPath + "/player.save");
        bf.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    private Vector3 PlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position;
    }

}
