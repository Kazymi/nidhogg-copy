using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Shield : MonoBehaviour, IDamageable,IShield
{
    [SerializeField] private ShieldConfig shieldConfig;
    
    private IInventory _inventory;
    private IShieldDeactivator _shieldDeactivator;
    private bool _isRegeneration;
    private float _currentHealth;
    private ShieldMenu _shieldMenu;

    private void Start()
    {
        _currentHealth = shieldConfig.Health;
    }

    public float CurrentShieldValue { get => _currentHealth/shieldConfig.Health; }
    public void TakeDamage(float damage)
    {
        StopAllCoroutines();
        StartCoroutine(StopRegen());
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = shieldConfig.Health/100*10;
            Dead();
        }
        _shieldMenu.UpdateSlider();
    }

    private void Update()
    {
        if (_isRegeneration)
        {
            _currentHealth += shieldConfig.RegenerationValue * Time.deltaTime;
            _shieldMenu.UpdateSlider();
        }

        if (_currentHealth > shieldConfig.Health)
        {
            _currentHealth = shieldConfig.Health;
        }
    }

    IEnumerator StopRegen()
    {
        _isRegeneration = false;
        yield return new WaitForSeconds(shieldConfig.TimeRegenAfterDamage);
        _isRegeneration = true;
    }
    
    public void Dead()
    {
        _shieldDeactivator.ShieldDeactivated?.Invoke();
        _inventory.CloseShield();
    }

    [Inject]
    private void Construct(IInventory inventory, IShieldDeactivator deactivator, ShieldMenu shieldMenu)
    {
        _inventory = inventory;
        _shieldMenu = shieldMenu;
        _shieldDeactivator = deactivator;
    }
}