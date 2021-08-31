using System.Collections.Generic;
using UnityEngine;

// TODO: can be made into C# class.
public class BulletManager : MonoBehaviour
{
    [SerializeField] private List<BulletConfiguration> bulletConfigurations;
    [SerializeField] private int countBullet;
    private Dictionary<BulletConfiguration, Factory> _factories = new Dictionary<BulletConfiguration, Factory>();

    private void Start()
    {
        foreach (var bullet in bulletConfigurations)
        {
            _factories.Add(bullet, new Factory(bullet.AmmoGameObject, countBullet, transform));
        }
    }

    public GameObject GetBulletByBulletConfiguration(BulletConfiguration bulletConfiguration)
    {
        Debug.Log(_factories.Count);
        return _factories[bulletConfiguration].Create();
    }
}
