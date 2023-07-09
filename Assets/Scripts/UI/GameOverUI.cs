using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private TextMeshProUGUI gameOverText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button mainMenuButton;

    public static event EventHandler onRestartClick;

    private int previousHighScore;

    private static bool isGameOver = false;

    public static bool IsGameOver
    {
        get => isGameOver;
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(HandleRestartClick);
        // restartButton.onClick.AddListener(() => Loader.LoadScene(Loader.ScenesEnum.GameScene));
        mainMenuButton.onClick.AddListener(() => Loader.LoadScene(Loader.ScenesEnum.MainMenuScene));
    }

    private void Start()
    {
        ui.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        Player.onGameOver += HandleGameOver;
        previousHighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void HandleGameOver(object sender, Player.onGameOverEventArgs e)
    {
        isGameOver = true;
        Time.timeScale = 0;

        gameOverText.text = e.message;

        if (e.score > previousHighScore)
        {
            PlayerPrefs.SetInt("HighScore", e.score);
            highScoreText.text = $"New High Score: {e.score}";
            highScoreText.gameObject.SetActive(true);
            previousHighScore = e.score;
        }
        else
        {
            highScoreText.gameObject.SetActive(false);
        }

        ui.SetActive(true);
    }

    private void HandleRestartClick()
    {
        onRestartClick?.Invoke(this, System.EventArgs.Empty);
        ui.SetActive(false);
        isGameOver = false;
        Time.timeScale = 1;
    }
}
