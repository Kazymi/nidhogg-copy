using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public abstract class Weapon : MonoBehaviour, IPolledObject
{
    [SerializeField] private WeaponClassName nameWeapon;
    [SerializeField] private int amountUse;
    
    private PlayerAnimatorController _playerAnimatorController;

    protected Transform _playerPivot;
    protected IInputHandler _inputHandler;
    protected BulletManager _bulletManager;
    protected int _currentAmountUse;
    protected bool _isActivated;
    
    public int CountUse => _currentAmountUse;
    public WeaponClassName WeaponName => nameWeapon;

    public bool IsActivated
    {
        set => _isActivated = value;
    }
    public Factory ParentFactory { get; set; }

    private void Start()
    {
        _currentAmountUse = amountUse;
    }

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

    public void Initialize(int amountUse)
    {
        _currentAmountUse = amountUse;
    }

    public void Destroy()
    {
        _currentAmountUse = amountUse;
        ParentFactory.Destroy(gameObject);
    }
}