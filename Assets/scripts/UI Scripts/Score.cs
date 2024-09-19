using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreBalloon;
    public int currentScore;
    public int trophyScore;
    public AudioSource scoreSound;
    private UIManager inventory;
    private bool scoreCall = false;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text trophyText;

    private void Start()
    {
        inventory = UIManager.instance;
        InitVariables();
    }
    private void InitVariables()
    {
        currentScore = 0;
        trophyScore = 0;
    }
   
    public void AddScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();

        //not using this anymore, need to add logic for prize shop
        //when currentScore = prize amount -> press E to spend points + add prize to inventory
        if (currentScore == 200) 
        {
            trophyScore += 1;
            trophyText.text = trophyScore.ToString();
            inventory.UpdateTrophyInventory(1);

        }
        scoreCall = true;
    }

    private void PlayScoreSound()
    {
        if (scoreCall)
        {
            scoreSound.Play();
            scoreCall = false;
        }
    }
}
