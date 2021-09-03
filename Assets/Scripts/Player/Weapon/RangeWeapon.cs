using System;
using UnityEngine;
using Zenject;

public class RangeWeapon : Weapon
{
    [SerializeField] private float fireRate;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletConfiguration bullet;
    
    private float _currentTimer;

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

    private void Update()
    {
        if (_isActivated == false)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0,90*_playerPivot.forward.x,0);
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
        if (_currentAmountUse <= 0 || _isActivated == false)
        {
            return;
        }
        if (_currentTimer < 0)
        {
            _currentTimer = fireRate;
            _currentAmountUse--;
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
