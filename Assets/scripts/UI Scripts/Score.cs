using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreBalloon;
    public int currentScore;
    [SerializeField] private Text scoreText;

    private void Start()
    {
        InitVariables();
    }
    private void InitVariables()
    {
        currentScore = 0;


    }
   
    public void AddScore()
    {
        currentScore += scoreBalloon;
        scoreText.text = currentScore.ToString();
    }
}
