using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private BulletConfiguration bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private BulletManager _bulletManager;
    private float _currentTimer;
    private IInputHandler _inputHandler;

    private void StartFire()
    {
        if (_currentTimer < 0)
        {
            _currentTimer = fireRate;
            Fire();
        }
    }

    private void Fire()
    {
        var newBullet = _bulletManager.GetBulletByBulletConfiguration(bullet);
        newBullet.transform.position = bulletSpawnPoint.position;
        newBullet.transform.rotation = bulletSpawnPoint.rotation;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,90*transform.forward.x,0);
        if (_currentTimer >= 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    [Inject]
    private void Construct(IInputHandler inputHandler,BulletManager bulletManager)
    {
        _bulletManager = bulletManager;
        _inputHandler = inputHandler;
        _inputHandler.Fire.Action += StartFire;
    }
}