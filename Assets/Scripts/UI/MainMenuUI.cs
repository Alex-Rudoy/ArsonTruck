using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button exitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            if (PlayerPrefs.HasKey("tutorialPassed"))
            {
                Loader.Instance.LoadScene(Loader.ScenesEnum.GameScene);
            }
            else
            {
                Loader.Instance.LoadScene(Loader.ScenesEnum.TutorialScene);
            }
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
