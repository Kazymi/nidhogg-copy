using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon configuration", fileName = "Weapon configuration")]
public class WeaponConfiguration : ScriptableObject
{
  [SerializeField] private WeaponName weaponName;
  [SerializeField] private Weapon mainWeapon;
  [SerializeField] private DroppedWeapon droppedWeapon;

  public WeaponName WeaponName => weaponName;

  public Weapon MainWeapon => mainWeapon;

  public DroppedWeapon DroppedWeapon => droppedWeapon;
}
