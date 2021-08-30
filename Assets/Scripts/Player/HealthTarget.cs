using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider))]
public class HealthTarget : MonoBehaviour, IDamageable
{
    [SerializeField] private DamageTarget damageTarget;

    private IPlayerHealth _playerHealth;

    [Inject]
    private void Construct(IPlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }
    public void TakeDamage(float damage)
    {
        _playerHealth.TakeDamage(damage,damageTarget);
    }
}
