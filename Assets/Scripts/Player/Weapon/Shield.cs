using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Shield : MonoBehaviour, IDamageable,IShield
{
    [SerializeField] private float shieldHealth;
    
    private IInventory _inventory;
    private ShieldMenu _shieldMenu;
    private float _currentHealth;
    public float CurrentShieldValue => _currentHealth/shieldHealth;
    
    private void Start()
    {
        _currentHealth = shieldHealth;
    }

    [Inject]
    private void Construct(IInventory inventory, ShieldMenu shieldMenu)
    {
        _inventory = inventory;
        _shieldMenu = shieldMenu;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
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