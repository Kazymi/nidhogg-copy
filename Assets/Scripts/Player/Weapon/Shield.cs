using System;
using System.Collections;
using UnityEngine;
using Zenject;

// TODO: clean up order for fields and methods
public class Shield : MonoBehaviour, IDamageable,IShield
{
    [SerializeField] private ShieldConfig shieldConfig;
    
    private IInventory _inventory;
    private ShieldMenu _shieldMenu;
    private bool _isRegeneration;
    private float _currentHealth;

    public float CurrentShieldValue => _currentHealth/shieldConfig.Health;

    [Inject]
    private void Construct(IInventory inventory, ShieldMenu shieldMenu)
    {
        _inventory = inventory;
        _shieldMenu = shieldMenu;
    }
    
    private void Start()
    {
        _currentHealth = shieldConfig.Health;
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
    
    public void TakeDamage(float damage)
    {
        StopAllCoroutines();
        StartCoroutine(StopRegen());
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _currentHealth = shieldConfig.Health/100*10;
            _isRegeneration = true;
            Dead();
        }
        _shieldMenu.UpdateSlider();
    }

    private IEnumerator StopRegen()
    {
        _isRegeneration = false;
        yield return new WaitForSeconds(shieldConfig.TimeRegenAfterDamage);
        _isRegeneration = true;
    }
    
    private void Dead()
    {
        _inventory.ShieldCrash?.Invoke();
        _inventory.CloseShield();
    }
    
}