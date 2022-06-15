using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public int MaxHealth = 100;

    [SerializeField] private int currentHealth;

    public int Health
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            onHealthChanged?.Invoke((float)currentHealth / MaxHealth);
        }
    }

    public UnityEvent onDead;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent onHit, onHeal;

    private void Start()
    {
        Health = MaxHealth;
    }

    public void Hit(int inflictedDamage)
    {
        Health -= inflictedDamage;
        if (Health <= 0)
        {
            onDead?.Invoke();
        }
        else
        {
            onHit?.Invoke();
        }
    }
}