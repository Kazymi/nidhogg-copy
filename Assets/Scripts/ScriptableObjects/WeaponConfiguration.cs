using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon configuration", fileName = "Weapon configuration")]
public class WeaponConfiguration : ScriptableObject
{
    [SerializeField] private WeaponClassName weaponName;
    [SerializeField] private Weapon mainWeapon;
    [SerializeField] private DroppedWeapon droppedWeapon;

    public WeaponClassName WeaponName => weaponName;
    public Weapon MainWeapon => mainWeapon;
    public DroppedWeapon DroppedWeapon => droppedWeapon;
}