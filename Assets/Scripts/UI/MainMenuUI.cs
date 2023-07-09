using System;
using UnityEngine;
using UnityEngine.UI;

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
            Loader.LoadScene(Loader.ScenesEnum.GameScene);
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
