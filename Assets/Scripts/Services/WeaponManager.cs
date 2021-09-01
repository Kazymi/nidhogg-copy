using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class WeaponManager
{
    private readonly Dictionary<WeaponName, Factory> _droppedWeapons = new Dictionary<WeaponName, Factory>();
    private readonly Dictionary<WeaponName, Factory> _mainWeapons = new Dictionary<WeaponName, Factory>();

    public WeaponManager(WeaponManagerConfig weaponManagerConfig)
    {
        foreach (var weaponConfiguration in weaponManagerConfig.WeaponConfigurations)
        {
            _droppedWeapons.Add(weaponConfiguration.WeaponName,
                new Factory(weaponConfiguration.DroppedWeapon.gameObject, 2, weaponManagerConfig.ParentTransform));

            _mainWeapons.Add(weaponConfiguration.WeaponName,
                new Factory(weaponConfiguration.MainWeapon.gameObject, 2, weaponManagerConfig.ParentTransform));
        }
    }

    public GameObject GetDroppedWeaponByWeaponName(WeaponName weaponName)
    {
        return _droppedWeapons[weaponName].Create();
    }

    public GameObject GetWeaponByWeaponName(WeaponName weaponName)
    {
        return _mainWeapons[weaponName].Create();
    }
}