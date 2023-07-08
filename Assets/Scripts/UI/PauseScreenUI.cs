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
    public static event EventHandler onOptionsClick;

    private static bool _isPaused = false;
    public static bool isPaused
    {
        get => _isPaused;
        private set
        {
            _isPaused = value;
            Time.timeScale = _isPaused ? 0 : 1;
        }
    }

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
        isPaused = !isPaused;
        ui.SetActive(isPaused);
    }
}
