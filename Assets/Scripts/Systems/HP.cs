using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class HP : MonoBehaviour
{
    public event EventHandler<OnHPChangeEventArgs> OnHPChange;

    public class OnHPChangeEventArgs : EventArgs
    {
        public int HP;

        public OnHPChangeEventArgs(int HP)
        {
            this.HP = HP;
        }
    }

    [SerializeField]
    private int maxHP;

    public int MaxHP
    {
        get => maxHP;
    }

    private int currentHP;

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

        if (gameObject.TryGetComponent<Car>(out Car car))
        {
            car.Explode(true);
        }
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        OnHPChange?.Invoke(this, new OnHPChangeEventArgs(currentHP));
    }
}
