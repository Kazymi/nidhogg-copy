using System.Collections.Generic;
using UnityEngine;

public class VFXManager
{
    private Dictionary<VFXConfiguration, Factory> _factories = new Dictionary<VFXConfiguration, Factory>();
    
    public VFXManager(List<VFXConfiguration> vfxConfigurations, Transform parent, int amountEffect)
    {
        foreach (var vfx in vfxConfigurations)
        {
            _factories.Add(vfx,new Factory(vfx.VFXEffectGameObject,amountEffect,parent));
        }       
    }

    public GameObject GetVFXByVFXConfiguration(VFXConfiguration vfxConfiguration)
    {
        return _factories[vfxConfiguration].Create();
    }
}