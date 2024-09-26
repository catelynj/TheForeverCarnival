using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    
    public int globalScore;
    public AudioSource pickupSource;
    public AudioSource cupSource;
    public AudioSource scoreSource;
    public AudioSource backgroundMusic;

    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = new GameObject("GameManager").AddComponent<GameManager>(); //create game manager object if required
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        globalScore = 0;
    }

    public void IncrementScore(int score)
    {
        globalScore += score;
        UIManager.instance.updateScoreCall = true;
        UIManager.instance.UpdateScore();
    }
}
