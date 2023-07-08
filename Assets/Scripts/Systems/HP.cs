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
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnHPChange?.Invoke(this, new OnHPChangeEventArgs { HP = currentHP });

        if (currentHP > 0)
            return;

        if (gameObject.TryGetComponent<Car>(out Car car))
        {
            car.Explode();
        }
    }
}
