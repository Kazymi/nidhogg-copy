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
    private VFXManager _vfxManager;
    private bool _isPlayerDead;

    public PlayerType PlayerType => _playerType;

    [Inject]
    private void Construct(IPlayerHealth playerHealth, IPlayerRespawnSystem respawnSystem, PlayerType playerType,
        VFXManager vfxManager)
    {
        _vfxManager = vfxManager;
        _playerHealth = playerHealth;
        _playerType = playerType;
        _playerHealth.PlayerDeath += target => _isPlayerDead = true;
        respawnSystem.RespawnAction += () => _isPlayerDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (_isPlayerDead)
        {
            return;
        }

        _playerHealth.TakeDamage(damage, damageTarget);
    }

    public void TakeDamage(float damage, VFXConfiguration vfxConfiguration)
    {
        var newVFX = _vfxManager.GetVFXByVFXConfiguration(vfxConfiguration);
        newVFX.transform.parent = null;
        newVFX.transform.position = transform.position;

        if (_isPlayerDead)
        {
            return;
        }

        _playerHealth.TakeDamage(damage, damageTarget);
    }
}