using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    
    private Dictionary<BulletConfiguration, Factory> _factories = new Dictionary<BulletConfiguration, Factory>();

    public BulletManager(BulletManagerConfiguration bulletConfigurations)
    {
        foreach (var bullet in bulletConfigurations.BulletConfigurations)
        {
            _factories.Add(bullet, new Factory(bullet.AmmoGameObject, bulletConfigurations.AmountBullet, bulletConfigurations.ParentTransform));
        }
    }

    public GameObject GetBulletByBulletConfiguration(BulletConfiguration bulletConfiguration)
    {
        Debug.Log(_factories.Count);
        return _factories[bulletConfiguration].Create();
    }
}
