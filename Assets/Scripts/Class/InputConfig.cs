
    using System;
    using UnityEngine;

    [Serializable]
    public class InputConfig
    {
        [SerializeField] private float clickThreshld;

        public float ClickThreshld => clickThreshld;
    }
