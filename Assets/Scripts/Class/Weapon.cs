using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public abstract class Weapon : MonoBehaviour, IPolledObject
{
    [SerializeField] private WeaponClassName nameWeapon;


    private IPlayerAnimatorController _playerAnimatorController;

    protected PlayerType _playerType;
    protected Transform _playerPivot;
    protected IInputHandler _inputHandler;
    protected BulletManager _bulletManager;
    private int _amountUse;
    protected int _currentAmounteUse;
    protected bool _isActivated;
    
    public int CurrentCountUse => _currentAmounteUse;
    public WeaponClassName WeaponName => nameWeapon;

    public bool IsActivated
    {
        set => _isActivated = value;
    }
    public Factory ParentFactory { get; set; }
    
    public void ActivateWeapon(Transform playerPivot)
    {
        _playerPivot = playerPivot;
        _playerAnimatorController.SetTrigger(nameWeapon.ToString(), false);
    }

    public virtual void Initialize(IInputHandler inputHandler, BulletManager bulletManager,
        IPlayerAnimatorController animatorController, PlayerType playerType)
    {
        _playerType = playerType;
        _playerAnimatorController = animatorController;
        _inputHandler = inputHandler;
        _bulletManager = bulletManager;
    }

    public void Initialize(int amountUse)
    {
        _currentAmounteUse = amountUse;
    }

    public void Destroy()
    {
        _currentAmounteUse = _amountUse;
        ParentFactory.Destroy(gameObject);
    }
}