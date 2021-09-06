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
       var newBullet= _factories[bulletConfiguration].Create();
       var iBullet = newBullet.GetComponent<IBullet>();
       if (iBullet != null)
       {
           iBullet.Initialize(bulletConfiguration);
       }

       return newBullet;
    }
}
