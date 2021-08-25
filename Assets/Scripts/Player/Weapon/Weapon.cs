using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    
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
     Debug.Log("fire");   
    }
    private void Update()
    {
        if (_currentTimer >= 0)
        {
            _currentTimer -= Time.deltaTime;
        }
    }

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.Fire.Action += StartFire;
    }
}
