using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroppedWeapon : MonoBehaviour,IPolledObject
{
    public Factory ParentFactory { get; set; }

    private bool _isUnlocked;
    private Rigidbody _rigidbody;
    private int _amountUse;
    private WeaponClassName _weaponClassName;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isUnlocked == false)
        {
            return;
        }
        var playerWeaponManager = other.GetComponent<PlayerWeaponManager>();
        if (playerWeaponManager)
        {
             playerWeaponManager.TakeWeapon(_weaponClassName,_amountUse);
             ParentFactory.Destroy(gameObject);
        }
    }

    public void Initialize(int amountUse, WeaponClassName weaponClassName)
    {
        _amountUse = amountUse;
        _weaponClassName = weaponClassName;
        _rigidbody.AddForce(transform.forward * 6,ForceMode.Impulse);
        if (amountUse <= 0)
        {
           ParentFactory.Destroy(gameObject);
           return;
        }
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        _isUnlocked = false;
        yield return new WaitForSeconds(1f);
        _isUnlocked = true;
    }
}