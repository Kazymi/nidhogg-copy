using System;
using UnityEngine;

[Serializable]
public class InventoryConfig
{
  [SerializeField] private Weapon weapon;
  [SerializeField] private Shield shield;
  [SerializeField] private int amountShields;

  public Weapon Weapon => weapon;

  public Shield Shield => shield;

  public int AmountShields => amountShields;
}