using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public static event EventHandler<onPlayerHPChangeEventArgs> onPlayerHPChange;

    public class onPlayerHPChangeEventArgs : EventArgs
    {
        public int HP;

        public onPlayerHPChangeEventArgs(int HP)
        {
            this.HP = HP;
        }
    }

    public static event EventHandler<onPlayerFuelChangeEventArgs> onPlayerFuelChange;

    public class onPlayerFuelChangeEventArgs : EventArgs
    {
        public float fuel;

        public onPlayerFuelChangeEventArgs(float fuel)
        {
            this.fuel = fuel;
        }
    }

    public static event EventHandler<onScoreChangeEventArgs> onScoreChange;

    public class onScoreChangeEventArgs : EventArgs
    {
        public int score;

        public onScoreChangeEventArgs(int score)
        {
            this.score = score;
        }
    }

    public static event EventHandler<onGameOverEventArgs> onGameOver;

    public class onGameOverEventArgs : EventArgs
    {
        public string message;
        public int score;

        public onGameOverEventArgs(string message, int score)
        {
            this.message = message;
            this.score = score;
        }
    }

    private float fuel;

    private int score;

    public int Score
    {
        get => score;
    }

    private void Start()
    {
        if (Instance != null)
        {
            throw new System.Exception("Multiple instances of Player!");
        }
        Instance = this;
        Car.onCarShotDown += HandleCarShotDown;
        House.onHouseShotDown += HandleHouseShotDown;
        PlayerControls.OnMilestoneReached += HandleMilestoneReached;
        gameObject.GetComponent<HP>().OnHPChange += HandleHpChange;
        GameOverUI.onRestartClick += HandleRestartClick;
        ResetValues();
    }

    private void OnDestroy()
    {
        Car.onCarShotDown -= HandleCarShotDown;
        PlayerControls.OnMilestoneReached -= HandleMilestoneReached;
        gameObject.GetComponent<HP>().OnHPChange -= HandleHpChange;
        GameOverUI.onRestartClick -= HandleRestartClick;
    }

    private void HandleRestartClick(object sender, System.EventArgs e)
    {
        ResetValues();
    }

    private void ResetValues()
    {
        fuel = 100;
        score = 0;
        gameObject.GetComponent<HP>().ResetHP();
    }

    private void Update()
    {
        fuel -= Time.deltaTime * 5;
        onPlayerFuelChange?.Invoke(this, new onPlayerFuelChangeEventArgs(fuel));

        if (fuel <= 0)
        {
            onGameOver?.Invoke(this, new onGameOverEventArgs("Out of fuel!", score));
        }
    }

    private void HandleHpChange(object sender, HP.OnHPChangeEventArgs e)
    {
        onPlayerHPChange?.Invoke(this, new onPlayerHPChangeEventArgs(e.HP));
        if (e.HP <= 0)
        {
            onGameOver?.Invoke(this, new onGameOverEventArgs("You died!", score));
        }
    }

    private void HandleCarShotDown(object sender, Car.OnCarShotDownEventArgs e)
    {
        fuel += e.fuelBonusForKill;
        onPlayerFuelChange?.Invoke(this, new onPlayerFuelChangeEventArgs(fuel));
        gameObject.GetComponent<HP>().Heal(e.hpBonusForKill);
    }

    private void HandleHouseShotDown(object sender, House.onHouseShotDownEventArgs e)
    {
        AddScore(e.scoreBonusForKill);
    }

    private void HandleMilestoneReached(object sender, PlayerControls.OnMilestoneReachedEventArgs e)
    {
        AddScore(1);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        onScoreChange?.Invoke(this, new onScoreChangeEventArgs(score));
    }
}
