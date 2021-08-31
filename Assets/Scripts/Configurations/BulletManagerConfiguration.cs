using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletManagerConfiguration
{
    [SerializeField] private List<BulletConfiguration> _bulletConfigurations;
    [SerializeField] private int amountBullet;
    [SerializeField] private Transform parentTransform;

    public List<BulletConfiguration> BulletConfigurations => _bulletConfigurations;
    public Transform ParentTransform => parentTransform;
    public int AmountBullet => amountBullet;
}