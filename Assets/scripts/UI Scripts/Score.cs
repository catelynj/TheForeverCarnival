using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreBalloon;
    public int currentScore;
    public int trophyScore;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text trophyText;

    private void Start()
    {
        InitVariables();
    }
    private void InitVariables()
    {
        currentScore = 0;
        trophyScore = 0;

    }
   
    public void AddScore()
    {
        currentScore += scoreBalloon;
        scoreText.text = currentScore.ToString();

        if(currentScore == 200)
        {
            trophyScore += 1;
            trophyText.text = trophyScore.ToString();
        }
    }


}
