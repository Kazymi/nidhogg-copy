using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    
    private Dictionary<BulletConfiguration, Factory> _factories = new Dictionary<BulletConfiguration, Factory>();

    public BulletManager(List<BulletConfiguration> bulletConfigurations, Transform parent,int amountBullet)
    {
        foreach (var bullet in bulletConfigurations)
        {
            _factories.Add(bullet, new Factory(bullet.AmmoGameObject, amountBullet, parent));
        }
    }

    public GameObject GetBulletByBulletConfiguration(BulletConfiguration bulletConfiguration)
    {
        Debug.Log(_factories.Count);
        return _factories[bulletConfiguration].Create();
    }
}
