using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreBalloon;
    public int currentScore;
    public int trophyScore;
    private AudioSource audioSource;
    private UIManager inventory;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text trophyText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventory = UIManager.instance;
        
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

        if(currentScore == 10)
        {
            trophyScore += 1;
            trophyText.text = trophyScore.ToString();
            Debug.Log("Play Sound.");
            if(audioSource != null )
            {
                audioSource.Play();
            }

            inventory.UpdateTrophyInventory(1);

        }
    }


}
