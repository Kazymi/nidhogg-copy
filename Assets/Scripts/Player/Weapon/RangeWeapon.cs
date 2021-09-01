using System;
using UnityEngine;
using Zenject;

public class RangeWeapon : Weapon
{
    [SerializeField] private float amountAmmo;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletConfiguration bullet;
    
    private float _currentTimer;
    private float _currentAmountAmmo;

    private void OnEnable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.Fire.Action += StartFire;
        }
    }

    private void OnDisable()
    {
        if (_inputHandler != null)
        {
            _inputHandler.Fire.Action -= StartFire;
        }
    }
    
    private void Start()
    {
        _currentAmountAmmo = amountAmmo;
    }
    
    public override void Update()
    {
        if (_currentTimer >= 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    public override void Initialize(IInputHandler inputHandler, BulletManager bulletManager, PlayerAnimatorController animatorController)
    {
        base.Initialize(inputHandler, bulletManager, animatorController);
        _inputHandler.Fire.Action += StartFire;
    }

    private void StartFire()
    {
        if (_currentAmountAmmo <= 0)
        {
            return;
        }
        if (_currentTimer < 0)
        {
            _currentTimer = fireRate;
            _currentAmountAmmo--;
            Fire();
        }
    }
    
    private void Fire()
    {
        var newBullet = _bulletManager.GetBulletByBulletConfiguration(bullet);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }

}
