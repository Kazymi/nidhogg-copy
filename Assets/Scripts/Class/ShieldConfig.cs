using System;
using UnityEngine;

[Serializable]
public class InventoryConfig
{
  [SerializeField] private Shield shield;
  [SerializeField] private int amountShields;

  public Shield Shield => shield;

  public int AmountShields => amountShields;
}