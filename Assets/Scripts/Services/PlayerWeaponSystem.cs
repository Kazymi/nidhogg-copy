using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerWeaponSystem : MonoBehaviour, IPlayerWeaponSystem
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
    private PlayerType _playerType;

    public Weapon CurrentWeapon => _currentWeapon;
    public bool IsCurrentWeaponMelee { get; private set; }

    [Inject]
    private void Construct(WeaponManager weaponManager, IPlayerAnimatorController playerAnimatorController,
        IInputHandler inputHandler, BulletManager bulletManager, IPlayerMovement playerMovement, PlayerType playerType,
        IPlayerHealth playerHealth, IPlayerRespawnSystem playerRespawnSystem)
    {
        _playerType = playerType;
        _inputHandler = inputHandler;
        _playerAnimatorController = playerAnimatorController;
        _bulletManager = bulletManager;
        _weaponManager = weaponManager;

        playerRespawnSystem.RespawnAction += () => _playerAnimatorController.SetTrigger(AnimationNameType.Hand.ToString(), false);
        playerHealth.PlayerDeath += target => DropAllWeapon();
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
        weapon.Initialize(_inputHandler, _bulletManager, _playerAnimatorController, _playerType);
        if (_currentWeapon == null)
        {
            _currentWeapon = weapon;
            SetCurrentWeapon(weapon);
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
                SetCurrentWeapon(weapon);
            }
        }
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
            _playerAnimatorController.SetTrigger(AnimationNameType.Hand.ToString(), false);
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
        weapon.gameObject.SetActive(true);
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
        _playerAnimatorController.SetTrigger(_currentWeapon.WeaponName.ToString(),false);
    }

    private void DropCurrentWeapon()
    {
        if (_currentWeapon == null)
        {
            return;
        }

        CreateDroppedWeapon(_currentWeapon.WeaponName, rightRangeHandTransform)
            .Initialize(_currentWeapon.CurrentCountUse, _currentWeapon.WeaponName);
        _currentWeapon.Destroy();
        _currentWeapon = null;
        StartCoroutine(CheckCurrentWeapon());
    }

    private void DropAllWeapon()
    {
        if (_weaponOnTheBack != null)
        {
            CreateDroppedWeapon(_weaponOnTheBack.WeaponName, spinePosition)
                .Initialize(_weaponOnTheBack.CurrentCountUse, _weaponOnTheBack.WeaponName);
            _weaponOnTheBack.Destroy();
            _weaponOnTheBack = null;
        }
        DropCurrentWeapon();
    }

    private DroppedWeapon CreateDroppedWeapon(WeaponClassName weaponClassName, Transform dropTransform)
    {
        var newDroppedWeapon = _weaponManager.GetDroppedWeaponByWeaponName(weaponClassName);
        newDroppedWeapon.transform.position = dropTransform.position;
        newDroppedWeapon.transform.rotation = dropTransform.rotation;
        newDroppedWeapon.transform.parent = null;
        return newDroppedWeapon.GetComponent<DroppedWeapon>();
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

    private IEnumerator CheckCurrentWeapon()
    {
        yield return null;
        if (_currentWeapon == null)
        {
            _playerAnimatorController.SetTrigger(AnimationNameType.Hand.ToString(), false);
        }
    }
}