using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class HealthTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageTarget damageTarget;
    
    private PlayerType _playerType;
    private IPlayerHealth _playerHealth;
    private Collider _collider;

    public PlayerType PlayerType => _playerType;

    [Inject]
    private void Construct(IPlayerHealth playerHealth, IPlayerRespawnSystem respawnSystem, PlayerType playerType)
    {
        _playerHealth = playerHealth;
        _playerType = playerType;
        playerHealth.PlayerDeath += target => _collider.enabled = false;
        respawnSystem.RespawnAction += () => _collider.enabled = true;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.TakeDamage(damage, damageTarget);
    }
}