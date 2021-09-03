using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class HealthTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageTarget damageTarget;
    
    private IPlayerHealth _playerHealth;
    private Collider _collider;
    
    [Inject]
    private void Construct(IPlayerHealth playerHealth,IPlayerRespawnSystem respawnSystem)
    {
        _playerHealth = playerHealth;

        playerHealth.PlayerDeath += target => _collider.enabled= false;
        respawnSystem.RespawnAction += () => _collider.enabled= true;
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void TakeDamage(float damage)
    {
        _playerHealth.TakeDamage(damage,damageTarget);
    }
}
