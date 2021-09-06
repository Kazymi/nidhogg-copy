using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DroppedWeapon : MonoBehaviour, IPolledObject
{
    public Factory ParentFactory { get; set; }

    private Rigidbody _rigidbody;
    private int _amountUse;
    private WeaponClassName _weaponClassName;

    public int AmountUse => _amountUse;
    public WeaponClassName WeaponClassName => _weaponClassName;
    
    public event Action<DroppedWeapon> OnDestroy;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(this);
        ParentFactory.Destroy(gameObject);
    }

    public void Initialize(int amountUse, WeaponClassName weaponClassName)
    {
        _amountUse = amountUse;
        _weaponClassName = weaponClassName;
        _rigidbody.AddForce(transform.forward * 6, ForceMode.Impulse);
        if (amountUse <= 0)
        {
            ParentFactory.Destroy(gameObject);
        }
    }
}