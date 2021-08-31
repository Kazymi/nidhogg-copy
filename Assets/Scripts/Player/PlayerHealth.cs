using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : IPlayerHealth
{
    private float _currentHealth;
    public Action<DamageTarget> PlayerDeath { get; set; }
    public Action PlayerTakeDamage { get; set; }
    public float MaxHealth { get; }
    public float CurrentHealth => _currentHealth;

    public PlayerHealth(float health)
    {
        _currentHealth = health;
        MaxHealth = health;
    }

    public void TakeDamage(float damage, DamageTarget damageTarget)
    {
        _currentHealth -= damage;
        PlayerTakeDamage?.Invoke();
        if (_currentHealth < 0)
        {
            PlayerDeath?.Invoke(damageTarget);
        }
    }
}