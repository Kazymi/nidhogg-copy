using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform spinePosition;
    [SerializeField] private Transform bodyTransform;
    
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

        inputHandler.DropWeaponAction.Action += DropCurrentWeapon;
    }

    private void Start()
    {
        if (startWeapon != null)
        {
            SetNewWeapon(startWeapon.WeaponName);
        }
    }

    public void SetActiveWeapon(bool isActivated)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(isActivated);
        }
    }

    public void TakeWeapon(WeaponClassName weaponName, int amountUse)
    {
        SetNewWeapon(weaponName).Initialize(amountUse);
    }
    
    private Weapon SetNewWeapon(WeaponClassName newWeapon)
    {
        var weapon = _weaponManager.GetWeaponByWeaponName(newWeapon).GetComponent<Weapon>();
        weapon.Initialize(_inputHandler,_bulletManager,_playerAnimatorController);
        if (_currentWeapon == null)
        {
            _currentWeapon = weapon;
        }
        else
        {
            if (_weaponOnTheBack == null)
            {
                _weaponOnTheBack = weapon;
            }
            else
            {
                DropCurrentWeapon();
                _currentWeapon = weapon;
            }
        }

        weapon.ActivateWeapon(bodyTransform);
        var weaponObject = weapon.gameObject;
        weaponObject.transform.parent = rightHandTransform;
        weaponObject.transform.position = rightHandTransform.position;
        return weapon;
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
        newDroppedWeapon.transform.position = rightHandTransform.position;
        newDroppedWeapon.transform.rotation = rightHandTransform.rotation;
        newDroppedWeapon.transform.parent = null;
        newDroppedWeapon.GetComponent<DroppedWeapon>().Initialize(_currentWeapon.CountUse,_currentWeapon.WeaponName);
        _currentWeapon.Destroy();
        _currentWeapon = null;
        _playerAnimatorController.SetTrigger(WeaponClassName.Hand.ToString(),false);
    }
}