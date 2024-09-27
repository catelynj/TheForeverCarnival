using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    public int globalScore;
   
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
}
