using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    [SerializeField]
    private Image timerImage;

    [SerializeField]
    private GameObject timerObject;

    private float timer = 5f;

    private bool timerFinished = false;

    private void Start()
    {
        continueButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timerFinished)
            return;

        if (timer <= 0)
        {
            timerFinished = true;
            timerObject.SetActive(false);

            continueButton.gameObject.SetActive(true);
            continueButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("tutorialPassed", 1);
                Loader.LoadScene(Loader.ScenesEnum.GameScene);
            });
            return;
        }

        timer -= Time.deltaTime;
        timerImage.fillAmount = (5 - timer) / 5f;
    }
}
