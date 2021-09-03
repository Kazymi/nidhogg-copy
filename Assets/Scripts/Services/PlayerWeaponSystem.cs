using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerWeaponSystem : MonoBehaviour,IPlayerWeaponSystem
{
    [SerializeField] private Transform rightRangeHandTransform;
    [SerializeField] private Transform rightMeleeHandTransform;
    [SerializeField] private Transform spinePosition;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private float takeWeaponRadius = 2f;
    [SerializeField] private Weapon startWeapon;
    [SerializeField] private Weapon secondStartWeapon;

    private Weapon _currentWeapon;
    private Weapon _weaponOnTheBack;
    private WeaponManager _weaponManager;
    private IInputHandler _inputHandler;
    private IPlayerAnimatorController _playerAnimatorController;
    private BulletManager _bulletManager;

    public bool IsCurrentWeaponMelee { get; private set; }
    
    [Inject]
    private void Construct(WeaponManager weaponManager, IPlayerAnimatorController playerAnimatorController,
        IInputHandler inputHandler, BulletManager bulletManager,IPlayerMovement playerMovement)
    {
        _inputHandler = inputHandler;
        _playerAnimatorController = playerAnimatorController;
        _bulletManager = bulletManager;
        _weaponManager = weaponManager;

        playerMovement.DefaultMovement += SetActiveWeapon;
        inputHandler.EquipAction.Action += TakeWeapon;
        inputHandler.DropWeaponAction.Action += DropCurrentWeapon;
        inputHandler.SwapWeaponAction.Action += SwipeWeapon;
    }

    private void Start()
    {
        if (startWeapon != null)
        {
            SetNewWeapon(startWeapon.WeaponName);
        }

        if (secondStartWeapon != null)
        {
            SetNewWeapon(secondStartWeapon.WeaponName);
        }
    }

    private void SetActiveWeapon(bool isActivated)
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(isActivated);
        }
    }

    private void TakeWeapon(WeaponClassName weaponName, int amountUse)
    {
        SetNewWeapon(weaponName).Initialize(amountUse);
    }

    private Weapon SetNewWeapon(WeaponClassName newWeapon)
    {
        var weapon = _weaponManager.GetWeaponByWeaponName(newWeapon).GetComponent<Weapon>();
        weapon.Initialize(_inputHandler, _bulletManager, _playerAnimatorController);
        if (_currentWeapon == null)
        {
            _currentWeapon = weapon;
        }
        else
        {
            if (_weaponOnTheBack == null)
            {
                _weaponOnTheBack = weapon;
                SetBackWeapon(_weaponOnTheBack);
                return weapon;
            }
            else
            {
                DropCurrentWeapon();
                _currentWeapon = weapon;
            }
        }

        SetCurrentWeapon(weapon);
        return weapon;
    }

    private void SwipeWeapon()
    {
        if (_currentWeapon == null && _weaponOnTheBack == null)
        {
            return;
        }

        if (_weaponOnTheBack == null)
        {
            SetBackWeapon(_currentWeapon);
            _currentWeapon = null;
            _playerAnimatorController.SetTrigger(WeaponClassName.Hand.ToString(), false);
        }
        else
        {
            if (_currentWeapon == null)
            {
                SetCurrentWeapon(_weaponOnTheBack);
                _weaponOnTheBack = null;
            }
            else
            {
                var currentWeapon = _currentWeapon;
                SetCurrentWeapon(_weaponOnTheBack);
                SetBackWeapon(currentWeapon);
            }
        }
    }

    private void SetBackWeapon(Weapon weapon)
    {
        _weaponOnTheBack = weapon;
        weapon.IsActivated = false;
        var transformWeapon = weapon.transform;
        transformWeapon.parent = spinePosition;
        transformWeapon.transform.localPosition = Vector3.zero;
        transformWeapon.transform.localRotation = Quaternion.identity;
    }

    private void SetCurrentWeapon(Weapon weapon)
    {
        IsCurrentWeaponMelee = weapon.WeaponName == WeaponClassName.Melee;
        _currentWeapon = weapon;
        weapon.ActivateWeapon(bodyTransform);
        weapon.IsActivated = true;
        var weaponObject = weapon.gameObject;
        var positionWeapon = rightRangeHandTransform;
        if (weapon.WeaponName == WeaponClassName.Melee)
        {
            positionWeapon = rightMeleeHandTransform;
        }

        weaponObject.transform.parent = positionWeapon;
        weaponObject.transform.localPosition = Vector3.zero;
        weaponObject.transform.localRotation = Quaternion.identity;
    }

    private void DropCurrentWeapon()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        var newDroppedWeapon = _weaponManager.GetDroppedWeaponByWeaponName(_currentWeapon.WeaponName);
        newDroppedWeapon.transform.position = rightRangeHandTransform.position;
        newDroppedWeapon.transform.rotation = rightRangeHandTransform.rotation;
        newDroppedWeapon.transform.parent = null;
        newDroppedWeapon.GetComponent<DroppedWeapon>().Initialize(_currentWeapon.CountUse, _currentWeapon.WeaponName);
        _currentWeapon.Destroy();
        _currentWeapon = null;
        _playerAnimatorController.SetTrigger(WeaponClassName.Hand.ToString(), false);
    }

    private void TakeWeapon()
    {
        var allFindObjects = Physics.OverlapSphere(transform.position, takeWeaponRadius);
        foreach (var findObject in allFindObjects)
        {
            var dropWeapon = findObject.GetComponent<DroppedWeapon>();
            if (dropWeapon != null)
            {
                TakeWeapon(dropWeapon.WeaponClassName, dropWeapon.AmountUse);
                dropWeapon.Destroy();
                return;
            }
        }
    }
}