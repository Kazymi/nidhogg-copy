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
    protected int _currentAmounteUse;
    protected bool _isActivated;
    
    public int CountUse => _currentAmounteUse;
    public WeaponClassName WeaponName => nameWeapon;

    public bool IsActivated
    {
        set => _isActivated = value;
    }
    public Factory ParentFactory { get; set; }

    private void Start()
    {
        _currentAmounteUse = amountUse;
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
        _currentAmounteUse = amountUse;
    }

    public void Destroy()
    {
        _currentAmounteUse = amountUse;
        ParentFactory.Destroy(gameObject);
    }
}