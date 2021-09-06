using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class HealthTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageTarget damageTarget;
    [SerializeField] private VFXConfiguration bleedingConfiguration;

    private PlayerType _playerType;
    private IPlayerHealth _playerHealth;
    private VFXManager _vfxManager;
    private bool _isPlayerDead;
    private Bleeding _bleeding;
    public PlayerType PlayerType => _playerType;

    [Inject]
    private void Construct(IPlayerHealth playerHealth, IPlayerRespawnSystem respawnSystem, PlayerType playerType,
        VFXManager vfxManager)
    {
        _vfxManager = vfxManager;
        _playerHealth = playerHealth;
        _playerType = playerType;
        _playerHealth.PlayerDeath += target => _isPlayerDead = true;
        respawnSystem.RespawnAction += () =>
        {
            _bleeding = new Bleeding(this, bleedingConfiguration, _vfxManager, transform);
            _isPlayerDead = false;
        };
    }
    
    private void Start()
    {
        _bleeding = new Bleeding(this, bleedingConfiguration, _vfxManager, transform);
    }

    private void Update()
    {
        _bleeding.Tick();
    }

    public void TakeDamage(DamageConfiguration damageConfiguration)
    {
        if (_isPlayerDead)
        {
            return;
        }

        if (damageConfiguration.VFXConfiguration != null)
        {
            var newVFX = _vfxManager.GetVFXByVFXConfiguration(damageConfiguration.VFXConfiguration);
            newVFX.transform.parent = null;
            newVFX.transform.position = transform.position;
        }

        if (damageConfiguration.Bleeding)
        {
            _bleeding.AddBleeding();
        }
        _playerHealth.TakeDamage(damageConfiguration.Damage, damageTarget);
    }
}