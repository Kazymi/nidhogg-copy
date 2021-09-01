
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class WeaponManagerConfig
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private List<WeaponConfiguration> weaponConfigurations;

        public Transform ParentTransform => parentTransform;

        public List<WeaponConfiguration> WeaponConfigurations => weaponConfigurations;
    }
