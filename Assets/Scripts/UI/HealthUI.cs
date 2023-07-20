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

    private Animator animator;

    private void Start()
    {
        Player.onPlayerHPChange += HandlePlayerHPChange;
        GameOverUI.onRestartClick += HandleRestartClick;
        animator = GetComponent<Animator>();
        ResetHpBar();
    }

    private void OnDestroy()
    {
        Player.onPlayerHPChange -= HandlePlayerHPChange;
        GameOverUI.onRestartClick -= HandleRestartClick;
    }

    private void Update()
    {
        UpdateFillPercent(
            Mathf.Lerp(healthBar.material.GetFloat("_Fill_percent"), targetHp, Time.deltaTime * 5)
        );
    }

    private void HandlePlayerHPChange(object sender, Player.onPlayerHPChangeEventArgs e)
    {
        targetHp = (float)e.HP / 100;
        animator.SetBool("IsLowHP", targetHp < 0.3);
        if (e.HP <= 0)
        {
            targetHp = 0;
            UpdateFillPercent(0);
        }
    }

    private void HandleRestartClick(object sender, EventArgs e)
    {
        ResetHpBar();
    }

    private void ResetHpBar()
    {
        targetHp = 100;
        UpdateFillPercent(1);
        animator.SetBool("IsLowHP", false);
    }

    private void UpdateFillPercent(float fillAmount)
    {
        healthBar.material.SetFloat("_Fill_percent", fillAmount);
    }
}
