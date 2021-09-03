using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerHealth : IPlayerHealth
{
    private float _currentHealth;
    public Action<DamageTarget> PlayerDeath { get; set; }
    public Action PlayerUpdateHealth { get; set; }
    public float MaxHealth { get; }
    public float CurrentHealth => _currentHealth;

    public PlayerHealth(float health, IPlayerRespawnSystem respawnSystem)
    {
        respawnSystem.RespawnAction += Respawn;
        _currentHealth = health;
        MaxHealth = health;
    }

    public void TakeDamage(float damage, DamageTarget damageTarget)
    {
        _currentHealth -= damage;
        PlayerUpdateHealth?.Invoke();
        if (_currentHealth <= 0)
        {
            PlayerDeath?.Invoke(damageTarget);
        }
    }

    private void Respawn()
    {
        _currentHealth = MaxHealth;
        PlayerUpdateHealth?.Invoke();
    }
}