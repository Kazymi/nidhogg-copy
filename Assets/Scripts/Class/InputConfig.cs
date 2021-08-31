
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    [Serializable]
    public class InputConfig
    {
       [SerializeField] private float clickThreshold;
       public float ClickThreshold => clickThreshold;
    }
