using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI topScoreText;

    private void Start()
    {
        Player.onScoreChange += HandleScoreChange;
        GameOverUI.onRestartClick += HandleRestart;
        ResetScore();
    }

    private void HandleScoreChange(object sender, Player.onScoreChangeEventArgs e)
    {
        scoreText.text = $"Score: {e.score}";
    }

    private void HandleRestart(object sender, EventArgs e)
    {
        ResetScore();
    }

    private void ResetScore()
    {
        scoreText.text = $"Score: 0";
        topScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
