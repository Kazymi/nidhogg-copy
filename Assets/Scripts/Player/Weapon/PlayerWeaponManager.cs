using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private PlayerWeaponManagerConfig _managerConfig;
    [SerializeField] private Weapon startWeapon;
    
    private Weapon _currentWeapon;
    private Weapon _weaponOnTheBack;
    private WeaponManager _weaponManager;

    private IInputHandler _inputHandler;
    private PlayerAnimatorController _playerAnimatorController;
    private BulletManager _bulletManager;

    [Inject]
    private void Construct(WeaponManager weaponManager,PlayerAnimatorController playerAnimatorController, IInputHandler inputHandler, BulletManager bulletManager)
    {
        _inputHandler = inputHandler;
        _playerAnimatorController = playerAnimatorController;
        _bulletManager = bulletManager;
        _weaponManager = weaponManager;
    }

    private void Start()
    {
        if (startWeapon != null)
        {
            SetNewWeapon(startWeapon);
        }
    }

    private void SetNewWeapon(Weapon newWeapon)
    {
        var weapon = _weaponManager.GetWeaponByWeaponName(newWeapon.WeaponName).GetComponent<Weapon>();
        weapon.Initialize(_inputHandler,_bulletManager,_playerAnimatorController);
        if (_weaponOnTheBack != null)
        {
            if (_currentWeapon == null)
            {
                _currentWeapon = weapon;
            }
            else
            {
                DropCurrentWeapon();
                _currentWeapon = weapon;
            }
        }
        else
        {
            _weaponOnTheBack = weapon;
        }
        weapon.SetWeapon(_managerConfig.BodyTransform);
        var weaponObject = weapon.gameObject;
        weaponObject.transform.parent = _managerConfig.RightHandTransform;
        weaponObject.transform.position = _managerConfig.RightHandTransform.position;
    }

    private void SetActiveWeapon(bool isActivated)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(isActivated);
        }
    }
    
    private void SwipeWeapon()
    {
        if (_weaponOnTheBack == null)
        {
            return;
        }
    }
    
    
    private void DropCurrentWeapon()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        var newDroppedWeapon = _weaponManager.GetDroppedWeaponByWeaponName(_currentWeapon.WeaponName);
        newDroppedWeapon.transform.position = _managerConfig.RightHandTransform.position;
        newDroppedWeapon.transform.rotation = _managerConfig.RightHandTransform.rotation;
        _currentWeapon.Destroy();
        _currentWeapon = null;
    }
}