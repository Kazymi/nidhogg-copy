
    using System;
    using UnityEngine;
    using Zenject;

    public abstract class Weapon : MonoBehaviour, IPolledObject
    {
        [SerializeField] private WeaponName nameWeapon;

        private PlayerAnimatorController _playerAnimatorController;
        private Transform _playerPivot;

        protected IInputHandler _inputHandler;
        protected BulletManager _bulletManager;
        public WeaponName WeaponName => nameWeapon;
        public Factory ParentFactory { get; set; }
        
        public virtual void Update()
        {
             transform.rotation = Quaternion.Euler(0,90*_playerPivot.forward.x,0);
        }

        public void SetWeapon(Transform playerPivot)
        {
            _playerPivot = playerPivot;
            _playerAnimatorController.SetTrigger(AnimationNameType.Weapon.ToString()+WeaponName,false);
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
