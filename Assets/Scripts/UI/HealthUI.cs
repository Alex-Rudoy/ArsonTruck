using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    private float targetHp = 100;

    private void Start()
    {
        Player.onPlayerHPChange += HandlePlayerHPChange;
        GameOverUI.onRestartClick += HandleRestartClick;
        ResetHpBar();
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetHp, Time.deltaTime * 5);
    }

    private void HandlePlayerHPChange(object sender, Player.onPlayerHPChangeEventArgs e)
    {
        targetHp = (float)e.HP / 100;
    }

    private void HandleRestartClick(object sender, EventArgs e)
    {
        ResetHpBar();
    }

    private void ResetHpBar()
    {
        targetHp = 100;
        healthBar.fillAmount = 1;
    }
}
