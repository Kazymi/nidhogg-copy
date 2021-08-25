using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CasualBullet : MonoBehaviour, IFactoryInitialize
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float flySpeed;
    [SerializeField] private float damage;

    private float _currentTime;

    public Factory ParentFactor
    {
        get => ParentFactor;
        set
        {
            _currentTime = lifeTime;
            ParentFactor = value;
        }
    }


    private void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;

         _currentTime -= Time.deltaTime;
        if (_currentTime < 0)
        {
           ParentFactor.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}