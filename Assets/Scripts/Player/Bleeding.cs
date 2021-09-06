using System;
using System.Collections;
using UnityEngine;

public class Bleeding
{
    private float _currentBleedingTime;
    private float _currentTime;
    private IDamageable _damageable;
    private VFXConfiguration _vfxConfiguration;
    private VFXManager _vfxManager;
    private Transform _parent;
    
    private const float _interval = 2f;
    private const float _damage = 0.1f;
    private const float _addBleedingTime = 12f;
    
    public Bleeding(IDamageable damageable, VFXConfiguration vfxConfiguration, VFXManager vfxManager, Transform parent)
    {
        _damageable = damageable;
        _vfxConfiguration = vfxConfiguration;
        _vfxManager = vfxManager;
        _parent = parent;
    }


    public void Tick()
    {
        if (_currentBleedingTime > 0)
        {
            if (_currentTime <= 0)
            {
                _damageable.TakeDamage(new DamageConfiguration(null,_damage,false));
                var bleedingVFX = _vfxManager.GetVFXByVFXConfiguration(_vfxConfiguration);
                bleedingVFX.transform.position = _parent.position;
                bleedingVFX.transform.rotation = _parent.rotation;
                _currentTime = _interval;
            }
            else
            {
                _currentTime -= Time.deltaTime;
            }

            _currentBleedingTime -= Time.deltaTime;
        }
    }

    public void AddBleeding()
    {
        _currentBleedingTime += _addBleedingTime;
    }
}