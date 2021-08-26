using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class CasualBullet : MonoBehaviour, IPolledObject
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float flySpeed;
    [SerializeField] private float damage;

    private float _currentTime;

    public Factory ParentFactory { get; set; }
    
    private void OnEnable()
    {
        StartCoroutine(DestroyTimer());
    }


    private void Update()
    {
        transform.position += transform.forward * flySpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        Destroy();
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy();
    }

    private void Destroy()
    {
        StopAllCoroutines();
        ParentFactory.Destroy(gameObject);
    }

}