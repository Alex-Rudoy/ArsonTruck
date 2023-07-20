using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class HP : MonoBehaviour
{
    public event EventHandler OnDeath;
    public event EventHandler<OnHPChangeEventArgs> OnHPChange;

    public class OnHPChangeEventArgs : EventArgs
    {
        public float HP;

        public OnHPChangeEventArgs(float HP)
        {
            this.HP = HP;
        }
    }

    [SerializeField]
    private float maxHP;

    public float MaxHP
    {
        get => maxHP;
    }

    private float currentHP;

    private void Start()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnHPChange?.Invoke(this, new OnHPChangeEventArgs(currentHP));

        if (currentHP > 0)
            return;

        OnDeath?.Invoke(this, new OnHPChangeEventArgs(currentHP));
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        OnHPChange?.Invoke(this, new OnHPChangeEventArgs(currentHP));
    }
}
