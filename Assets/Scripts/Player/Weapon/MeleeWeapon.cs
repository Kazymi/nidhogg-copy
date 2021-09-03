using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class MeleeWeapon : Weapon
{
    [SerializeField] private bool _isActivatedDamageDealer;
    

    public bool IsActivatedDamageDealer
    {
        set => _isActivatedDamageDealer = value;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isActivatedDamageDealer == false)
        {
            return;
        }

        var ht = other.GetComponent<HealthTarget>();
        if (ht)
        {
            if (ht.PlayerType != _playerType)
            {
                ht.TakeDamage(100f);
            }
        }
    }
}