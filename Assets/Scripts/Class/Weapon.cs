using System;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour, IPolledObject
{
    [SerializeField] private WeaponClassName nameWeapon;

    private PlayerAnimatorController _playerAnimatorController;

    protected Transform _playerPivot;
    protected IInputHandler _inputHandler;
    protected BulletManager _bulletManager;
    public WeaponClassName WeaponName => nameWeapon;
    public Factory ParentFactory { get; set; }


    public void ActivateWeapon(Transform playerPivot)
    {
        _playerPivot = playerPivot;
        _playerAnimatorController.SetTrigger(nameWeapon.ToString(), false);
    }

    public virtual void Initialize(IInputHandler inputHandler, BulletManager bulletManager,
        PlayerAnimatorController animatorController)
    {
        _playerAnimatorController = animatorController;
        _inputHandler = inputHandler;
        _bulletManager = bulletManager;
    }

    public void Destroy()
    {
        ParentFactory.Destroy(gameObject);
    }
}