
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager
{
    private readonly Dictionary<WeaponClassName, Factory> _droppedWeapons = new Dictionary<WeaponClassName, Factory>();
    private readonly Dictionary<WeaponClassName, Factory> _mainWeapons = new Dictionary<WeaponClassName, Factory>();
    private Dictionary<WeaponClassName, int> _amountsUse = new Dictionary<WeaponClassName, int>();
    public WeaponManager(Transform parent, List<WeaponConfiguration> weaponConfigurations)
    {
        foreach (var weaponConfiguration in weaponConfigurations)
        {
            _amountsUse.Add(weaponConfiguration.WeaponName,weaponConfiguration.AmountUse);
            _droppedWeapons.Add(weaponConfiguration.WeaponName,
                new Factory(weaponConfiguration.DroppedWeapon.gameObject, 2, parent));

            weaponConfiguration.MainWeapon.Initialize(weaponConfiguration.AmountUse);
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
        var returnWeapon = _mainWeapons[weaponName].Create();
        returnWeapon.GetComponent<Weapon>().Initialize(_amountsUse[weaponName]);
        return returnWeapon;
    }

    public int GetAmountUseByType(WeaponClassName weaponClassName)
    {
        return _amountsUse[weaponClassName];
    }
}