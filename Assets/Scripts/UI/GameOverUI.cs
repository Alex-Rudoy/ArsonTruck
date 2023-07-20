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
    private Button restartButton;

    [SerializeField]
    private Button mainMenuButton;

    public static event EventHandler onRestartClick;

    private static bool isGameOver = false;

    public static bool IsGameOver
    {
        get => isGameOver;
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(HandleRestartClick);
        mainMenuButton.onClick.AddListener(
            () => Loader.Instance.LoadScene(Loader.ScenesEnum.MainMenuScene)
        );
    }

    private void Start()
    {
        ui.SetActive(false);
        Player.onGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        Player.onGameOver -= HandleGameOver;
    }

    private void HandleGameOver(object sender, Player.onGameOverEventArgs e)
    {
        isGameOver = true;
        Time.timeScale = 0;

        gameOverText.text = e.message;

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
