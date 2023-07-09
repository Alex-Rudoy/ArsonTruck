using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button mainMenuButton;

    public static event EventHandler onResumeClick;

    private static bool isPaused = false;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => onResumeClick?.Invoke(this, System.EventArgs.Empty));
        mainMenuButton.onClick.AddListener(() => Loader.LoadScene(Loader.ScenesEnum.MainMenuScene));
    }

    private void Start()
    {
        GameInputManager.onPauseAction += TogglePause;
        onResumeClick += TogglePause;
        ui.SetActive(false);
    }

    private void OnDestroy()
    {
        GameInputManager.onPauseAction -= TogglePause;
        onResumeClick -= TogglePause;
    }

    private void TogglePause(object sender, System.EventArgs e)
    {
        if (GameOverUI.IsGameOver)
            return;

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        ui.SetActive(isPaused);
    }
}
