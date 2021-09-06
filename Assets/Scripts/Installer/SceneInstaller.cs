﻿
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class SceneInstaller : MonoInstaller
    {

        [SerializeField] private Transform bulletParentTransform;
        [SerializeField] private List<BulletConfiguration> bulletConfigurations;
        [SerializeField] private int amountBullet;

        [SerializeField] private Transform weaponParentTransform;
        [SerializeField] private List<WeaponConfiguration> weaponConfigurations;

        [SerializeField] private Transform vfxParentTransform;
        [SerializeField] private List<VFXConfiguration> vfxConfigurations;
        [SerializeField] private int amountVfx;
        
        public override void InstallBindings()
        {
            var bulletManager = new BulletManager(bulletConfigurations, bulletParentTransform, amountBullet);
            var weaponManager = new WeaponManager(weaponParentTransform, weaponConfigurations);
            var vfxManager = new VFXManager(vfxConfigurations, vfxParentTransform, amountVfx);
            
            Container.Bind<WeaponManager>().FromInstance(weaponManager).AsSingle();
            Container.Bind<BulletManager>().FromInstance(bulletManager).AsSingle();
            Container.Bind<VFXManager>().FromInstance(vfxManager).AsSingle();
        }
    }
