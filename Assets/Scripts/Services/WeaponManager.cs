using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class WeaponManager
{
    private readonly Dictionary<WeaponClassName, Factory> _droppedWeapons = new Dictionary<WeaponClassName, Factory>();
    private readonly Dictionary<WeaponClassName, Factory> _mainWeapons = new Dictionary<WeaponClassName, Factory>();

    public WeaponManager(Transform parent, List<WeaponConfiguration> weaponConfigurations)
    {
        foreach (var weaponConfiguration in weaponConfigurations)
        {
            _droppedWeapons.Add(weaponConfiguration.WeaponName,
                new Factory(weaponConfiguration.DroppedWeapon.gameObject, 2, parent));

            _mainWeapons.Add(weaponConfiguration.WeaponName,
                new Factory(weaponConfiguration.MainWeapon.gameObject, 2, parent));
        }
    }

    public GameObject GetDroppedWeaponByWeaponName(WeaponClassName weaponName)
    {
        return _droppedWeapons[weaponName].Create();
    }

    public GameObject GetWeaponByWeaponName(WeaponClassName weaponName)
    {
        return _mainWeapons[weaponName].Create();
    }
}