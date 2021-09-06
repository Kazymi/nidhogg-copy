using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Shield : MonoBehaviour, IDamageable,IShield
{
    [SerializeField] private float shieldHealth;
    
    private IShieldSystem _inventory;
    private IShieldMenu _shieldMenu;
    private float _currentHealth;
    public float CurrentShieldValue => _currentHealth/shieldHealth;
    
    private void Start()
    {
        _currentHealth = shieldHealth;
    }

    [Inject]
    private void Construct(IShieldSystem inventory, IShieldMenu shieldMenu, IPlayerRespawnSystem playerRespawnSystem)
    {
        playerRespawnSystem.RespawnAction += () =>
        {
            _currentHealth = shieldHealth;
        };
        _inventory = inventory;
        _shieldMenu = shieldMenu;
    }

    public void TakeDamage(DamageConfiguration damageConfiguration)
    {
        _currentHealth -= damageConfiguration.Damage;
        if (_currentHealth <= 0)
        {
            Dead();
        }
        _shieldMenu.UpdateSlider();
    }

    private void Dead()
    {
        _currentHealth = shieldHealth;
        _inventory.ShieldCrash?.Invoke();
        _inventory.CloseShield();
    }
    
}