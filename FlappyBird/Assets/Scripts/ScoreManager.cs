using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;


    public int score;
    public int highscore;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highscore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            highscore = 0;
        }
    }

    public void AddScore()
    {
        score++;

        UIController.Instance.UpdateScore(score);
        
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("HighScore", highscore);
        }
    }
}
